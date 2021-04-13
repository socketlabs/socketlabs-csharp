using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable MethodSupportsCancellation
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
                return await HttpClient
                    .PostAsync(EndpointUrl, content, cancellationToken)
                    .ConfigureAwait(false);
            
            var attempts = 0;
            do
            {
                var waitInterval = RetrySettings.GetNextWaitInterval(attempts);

                try
                {
                    var response = await HttpClient.PostAsync(EndpointUrl, content, cancellationToken)
                        .ConfigureAwait(false);

                    if (ErrorStatusCodes.Contains(response.StatusCode))
                        throw new HttpRequestException(
                            $"HttpStatusCode: '{response.StatusCode}'. Response contains server error.");

                    return response;
                }
                catch (TaskCanceledException)
                {
                    attempts++;
                    if (attempts > RetrySettings.MaximumNumberOfRetries) throw new TimeoutException();
                    await Task.Delay(waitInterval).ConfigureAwait(false);
                }
                catch (HttpRequestException)
                {
                    attempts++;
                    if (attempts > RetrySettings.MaximumNumberOfRetries) throw;
                    await Task.Delay(waitInterval).ConfigureAwait(false);
                }

            } while (true);

        }



    }
}
