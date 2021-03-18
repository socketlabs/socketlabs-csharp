using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SocketLabs.InjectionApi.Core
{
    internal class RetryHandler 
    {

        private readonly HttpClient HttpClient;
        private readonly string EndpointUrl;
        private readonly RetrySettings RetrySettings;

        private readonly List<HttpStatusCode> ErrorStatusCodes = new List<HttpStatusCode>()
        {
            HttpStatusCode.InternalServerError,
            HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.GatewayTimeout
        };
        
        /// <summary>
        /// Creates a new instance of the <c>RetryHandler</c>.
        /// </summary>
        /// <param name="httpClient">A <c>HttpClient</c> instance</param>
        /// <param name="endpointUrl">The SocketLabs Injection API endpoint Url</param>
        /// <param name="settings">A <c>RetrySettings</c> instance</param>
        public RetryHandler(HttpClient httpClient, string endpointUrl, RetrySettings settings)
        {
            HttpClient = httpClient;
            EndpointUrl = endpointUrl;
            RetrySettings = settings;
        }

        
        public async Task<HttpResponseMessage> SendAsync(StringContent content, CancellationToken cancellationToken)
        {
            if (RetrySettings.MaximumNumberOfRetries == 0)
            {
                return await HttpClient.PostAsync(EndpointUrl, content, cancellationToken)
                    .ConfigureAwait(false);
            }

            HttpResponseMessage response = null;

            var numberOfAttempts = 0;
            var sent = false;

            while (!sent)
            {
                var waitFor = this.GetNextWaitInterval(numberOfAttempts);

                try
                {
                    response = await HttpClient.PostAsync(EndpointUrl, content, cancellationToken).ConfigureAwait(false);
                    
                    if (ErrorStatusCodes.Contains(response.StatusCode))
                        throw new HttpRequestException($"HttpStatusCode: '{response.StatusCode}'. Response contains server error.");
                    

                    sent = true;
                }
                catch (TaskCanceledException)
                {
                    numberOfAttempts++;

                    if (numberOfAttempts > RetrySettings.MaximumNumberOfRetries)
                    {
                        throw new TimeoutException();
                    }

                    // ReSharper disable once MethodSupportsCancellation, cancel will be indicated on the token
                    await Task.Delay(waitFor).ConfigureAwait(false);
                }
                catch (HttpRequestException)
                {
                    numberOfAttempts++;

                    if (numberOfAttempts > RetrySettings.MaximumNumberOfRetries)
                    {
                        throw;
                    }

                    await Task.Delay(waitFor).ConfigureAwait(false);
                }
            }

            return response;
        }



        internal virtual int GetRetryDelta(int numberOfAttempts)
        {
            var random = new Random();

            var min = (int) (TimeSpan.FromSeconds(1).TotalMilliseconds * 0.8);
            var max = (int) (TimeSpan.FromSeconds(1).TotalMilliseconds * 1.2);

            return (int) ((Math.Pow(2.0, numberOfAttempts) - 1.0) * random.Next(min, max));
        }

        private TimeSpan GetNextWaitInterval(int numberOfAttempts)
        {
            var interval = (int)Math.Min(
                RetrySettings.MinimumRetryTimeBetween.TotalMilliseconds + GetRetryDelta(numberOfAttempts),
                RetrySettings.MaximumRetryTimeBetween.TotalMilliseconds);

            return TimeSpan.FromMilliseconds(interval);
        }

    }
}
