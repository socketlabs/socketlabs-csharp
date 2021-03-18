using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using SocketLabs.InjectionApi.Core;
using SocketLabs.InjectionApi.Message;

namespace SocketLabs.InjectionApi
{
    /// <summary>
    /// SocketLabsClient is a wrapper for the SocketLabs Injection API that makes 
    /// it easy to send messages and parse responses.
    /// </summary>
    /// <example>
    /// <code>
    /// var client = new SocketLabsClient(00000, "apiKey");
    /// 
    /// var message = new BasicMessage();
    ///
    /// // Build your message
    /// 
    /// var response = client.Send(message);
    /// 
    /// if (response.Result != SendResult.Success)
    /// {
    ///     // Handle Error
    /// }
    ///</code>
    /// </example>
    public class SocketLabsClient : ISocketLabsClient, IDisposable
    {
        private string UserAgent { get; } = $"SocketLabs-csharp/{typeof(SocketLabsClient).GetTypeInfo().Assembly.GetName().Version}";

        private readonly int _serverId;
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        
        /// <summary>
        /// The SocketLabs Injection API endpoint Url
        /// </summary>
        public string EndpointUrl { get; set; } = "https://inject.socketlabs.com/api/v1/email";

        /// <summary>
        /// A timeout period for the Injection API request (in Seconds). Default: 120s
        /// </summary>
        public int RequestTimeout { get; set; } = 120;
        
        /// <summary>
        /// Creates a new instance of the <c>SocketLabsClient</c>.
        /// </summary>
        /// <param name="serverId">Your SocketLabs ServerId number.</param>
        /// <param name="apiKey">Your SocketLabs Injection API key.</param>
        public SocketLabsClient(int serverId, string apiKey)
        {
            _serverId = serverId;
            _apiKey = apiKey;
            _httpClient = BuildHttpClient(null);
        }
        
        /// <summary>
        /// Creates a new instance of the <c>SocketLabsClient</c> with a proxy.
        /// </summary>
        /// <param name="serverId">Your SocketLabs ServerId number.</param>
        /// <param name="apiKey">Your SocketLabs Injection API key.</param>
        /// <param name="optionalProxy">The WebProxy you would like to use.</param>
         public SocketLabsClient(int serverId, string apiKey, IWebProxy optionalProxy)
        {
            _serverId = serverId;
            _apiKey = apiKey;
            _httpClient = BuildHttpClient(optionalProxy);

        }

        /// <summary>
        /// Creates a new instance of the <c>SocketLabsClient</c> with a provided HttpClient.
        /// </summary>
        /// <param name="serverId">Your SocketLabs ServerId number.</param>
        /// <param name="apiKey">Your SocketLabs Injection API key.</param>
        /// <param name="httpClient">The HttpClient instance you would like to use.</param>
        public SocketLabsClient(int serverId, string apiKey, HttpClient httpClient)
        {
            _serverId = serverId;
            _apiKey = apiKey;

            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            ConfigureHttpClient(httpClient);
        }

        private HttpClient BuildHttpClient(IWebProxy optionalProxy)
        {
            var httpClient =  optionalProxy != null ? new HttpClient(new HttpClientHandler() { UseProxy = true, Proxy = optionalProxy}) : new HttpClient();
            ConfigureHttpClient(httpClient);
            return httpClient;
        }

        private void ConfigureHttpClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        }

        /// <summary>
        /// Quickly and easily send a basic message without the need to build up a message object
        /// or instantiate a SocketLabsClient.
        /// </summary>
        /// <param name="serverId">Your SocketLabs ServerId number.</param>
        /// <param name="apiKey">Your SocketLabs Injection API key.</param>
        /// <param name="toAddress">The To address for your message recipient.</param>
        /// <param name="fromAddress">The From address to be used for your message.</param>
        /// <param name="subject">The subject for your message.</param>
        /// <param name="htmlContent">The Html content for your message.</param>
        /// <param name="textContent">The plain text content for your message.</param>
        /// <remarks>The message must contain either htmlContent, textContent, or both to be valid.</remarks>
        /// <returns>A <c>SendResponse</c> of an SocketLabsClient send request.</returns>
        /// <example>
        /// This sample shows you how to send a message using QuickSend
        /// <code>
        /// var response = SocketLabsClient.QuickSend(000000, "apiKey",
        ///     "recipient@example.com",
        ///     "from@example.com",
        ///     "Lorem Ipsum",
        ///     "<html>Lorem Ipsum</html>",
        ///     "Lorem Ipsum"
        /// );
        /// </code>
        /// </example>
        public static SendResponse QuickSend(
            int serverId, 
            string apiKey, 
            string toAddress, 
            string fromAddress, 
            string subject, 
            string htmlContent, 
            string textContent)
        {
            var client = new SocketLabsClient(serverId, apiKey);
 
            var email = new BasicMessage
            {
                Subject = subject,
                To =   {
                    new EmailAddress(toAddress)
                },
                From = new EmailAddress(fromAddress),
                HtmlBody = htmlContent,
                PlainTextBody = textContent
            };

            return client.Send(email);
        }

        /// <summary>
        /// Quickly and easily send a basic message without the need to build up a message object
        /// or instantiate a SocketLabsClient.
        /// </summary>
        /// <param name="serverId">Your SocketLabs ServerId number.</param>
        /// <param name="apiKey">Your SocketLabs Injection API key.</param>
        /// <param name="toAddress">The To address for your message recipient.</param>
        /// <param name="fromAddress">The From address to be used for your message.</param>
        /// <param name="subject">The subject for your message.</param>
        /// <param name="content">The content of your message.</param>
        /// <param name="isHtml">Use true for Html messages, false for plain text messages.</param> 
        /// <returns>A <c>SendResponse</c> of an SocketLabsClient send request.</returns>
        /// <example>
        /// This sample shows you how to send a message using QuickSend
        /// <code>
        /// var response = SocketLabsClient.QuickSend(000000, "apiKey",
        ///     "recipient@example.com",
        ///     "from@example.com",
        ///     "Lorem Ipsum",
        ///     "<html>Lorem Ipsum</html>",
        ///     true
        /// );
        /// </code>
        /// </example>
        public static SendResponse QuickSend(
            int serverId, 
            string apiKey, 
            string toAddress, 
            string fromAddress, 
            string subject, 
            string content, 
            bool isHtml = false)
        {
            var html = isHtml ? content : null;
            var text = isHtml ? null: content; 

            return QuickSend(serverId, apiKey, toAddress, fromAddress, subject, html, text); 
        }

        /// <summary>
        /// Asynchronously sends a basic email message and returns the response from the Injection API.
        /// </summary>
        /// <param name="message">A <c>BasicMessage</c> object to be sent.</param> 
        /// <param name="cancellationToken">A <c>CancellationToken</c> to handle cancellation between async threads.</param> 
        /// <returns>A <c>SendResponse</c> of an SocketLabsClient send request.</returns>
        /// <example>
        /// This sample shows you how to Send a Basic Message
        /// <code>
        /// var client = new SocketLabsClient(00000, "apiKey");
        /// 
        /// var message = new BasicMessage();
        ///
        /// message.PlainTextBody = "This is the body of my message sent to ##Name##";
        /// message.HtmlBody = "<html>This is the HtmlBody of my message sent to ##Name##</html>";
        /// message.Subject = "Sending a test message";
        /// message.From.Email = "from@example.com"; 
        /// message.To.Add("recipient1@example.com"); 
        /// message.To.Add("recipient2@example.com");
        /// 
        /// var response = await client.Send(message);
        /// 
        /// if (response.Result != SendResult.Success)
        /// {
        ///     // Handle Error
        /// }
        ///</code>
        /// </example>
        public async Task<SendResponse> SendAsync(IBasicMessage message, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new SendValidator();

                var validationResult = validator.ValidateCredentials(_serverId, _apiKey);
                if (validationResult.Result != SendResult.Success) return validationResult;

                validationResult = validator.ValidateMessage(message);
                if (validationResult.Result != SendResult.Success) return validationResult;

                var factory = new InjectionRequestFactory(_serverId, _apiKey);
                var injectionRequest = factory.GenerateRequest(message);
                var json = injectionRequest.GetAsJson();

                _httpClient.Timeout = TimeSpan.FromSeconds(RequestTimeout);
                var httpResponse = await _httpClient.PostAsync(EndpointUrl, json, cancellationToken);
                
                var response = new InjectionResponseParser().Parse(httpResponse);
                return response;
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                throw new TimeoutException();
            }
        }

        /// <summary>
        /// Asynchronously sends a bulk email message and returns the response from the Injection API.
        /// </summary>
        /// <param name="message">A <c>BulkMessage</c> object to be sent.</param>
        /// <param name="cancellationToken">A <c>CancellationToken</c> to handle cancellation between async threads.</param> 
        /// <returns>A <c>SendResponse</c> of an SocketLabsClient send request.</returns>
        /// <example>
        /// This sample shows you how to Send a Bulk Message
        /// <code>
        /// var client = new SocketLabsClient(00000, "apiKey");
        /// 
        /// var message = new BulkMessage();
        ///
        /// message.PlainTextBody = "This is the body of my message sent to ##Name##";
        /// message.HtmlBody = "<html>This is the HtmlBody of my message sent to ##Name##</html>";
        /// message.Subject = "Sending a test message";
        /// message.From.Email = "from@example.com";
        /// 
        /// var recipient1 = message.To.Add("recipient1@example.com");
        /// recipient1.MergeData.Add("Name", "Recipient1");
        /// 
        /// var recipient2 = message.To.Add("recipient2@example.com");
        /// recipient2.MergeData.Add("Name", "Recipient2");
        /// 
        /// var response = await client.Send(message);
        /// 
        /// if (response.Result != SendResult.Success)
        /// {
        ///     // Handle Error
        /// }
        ///</code>
        /// </example>
        public async Task<SendResponse> SendAsync(IBulkMessage message, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new SendValidator();

                var validationResult = validator.ValidateCredentials(_serverId, _apiKey);
                if (validationResult.Result != SendResult.Success) return validationResult;

                validationResult = validator.ValidateMessage(message);
                if (validationResult.Result != SendResult.Success) return validationResult;

                var factory = new InjectionRequestFactory(_serverId, _apiKey);
                var injectionRequest = factory.GenerateRequest(message);

                _httpClient.Timeout = TimeSpan.FromSeconds(RequestTimeout);
                var httpResponse = await _httpClient.PostAsync(EndpointUrl, injectionRequest.GetAsJson(), cancellationToken);

                var response = new InjectionResponseParser().Parse(httpResponse);
                return response;
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                throw new TimeoutException();
            }
        }

        /// <summary>
        /// Synchronously sends a basic email message and returns the response from the Injection API.
        /// </summary>
        /// <param name="message">A <c>BasicMessage</c> object to be sent.</param> 
        /// <returns>A <c>SendResponse</c> of an SocketLabsClient send request.</returns>
        /// <example>
        /// This sample shows you how to Send a Basic Message
        /// <code>
        /// var client = new SocketLabsClient(00000, "apiKey");
        /// 
        /// var message = new BasicMessage();
        ///
        /// message.PlainTextBody = "This is the body of my message sent to ##Name##";
        /// message.HtmlBody = "<html>This is the HtmlBody of my message sent to ##Name##</html>";
        /// message.Subject = "Sending a test message";
        /// message.From.Email = "from@example.com"; 
        /// message.To.Add("recipient1@example.com"); 
        /// message.To.Add("recipient2@example.com");
        /// 
        /// var response = client.Send(message);
        /// 
        /// if (response.Result != SendResult.Success)
        /// {
        ///     // Handle Error
        /// }
        ///</code>
        /// </example>
        public SendResponse Send(IBasicMessage message)
        {
            try
            {
                var source = new CancellationTokenSource();
                //Read this if you have questions: https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
                var sendTask = Task.Run(() => SendAsync(message, source.Token));
                
                while (!sendTask.IsCompleted) { }
                if (sendTask.Status == TaskStatus.Faulted) throw sendTask.Exception;
                
                return sendTask.Result;
                
            }
            //for synchronous usage, try to simplify exceptions being thrown
            catch (AggregateException e)
            {
                if (e.InnerException != null)
                    ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Synchronously sends a bulk email message and returns the response from the Injection API.
        /// </summary>
        /// <param name="message">A <c>BulkMessage</c> object to be sent.</param>
        /// <returns>A <c>SendResponse</c> of an SocketLabsClient send request.</returns>
        /// <example>
        /// This sample shows you how to Send a Bulk Message
        /// <code>
        /// var client = new SocketLabsClient(00000, "apiKey");
        /// 
        /// var message = new BulkMessage();
        ///
        /// message.PlainTextBody = "This is the body of my message sent to ##Name##";
        /// message.HtmlBody = "<html>This is the HtmlBody of my message sent to ##Name##</html>";
        /// message.Subject = "Sending a test message";
        /// message.From.Email = "from@example.com";
        /// 
        /// var recipient1 = message.To.Add("recipient1@example.com");
        /// recipient1.MergeData.Add("Name", "Recipient1");
        /// 
        /// var recipient2 = message.To.Add("recipient2@example.com");
        /// recipient2.MergeData.Add("Name", "Recipient2");
        /// 
        /// var response = client.Send(message);
        /// 
        /// if (response.Result != SendResult.Success)
        /// {
        ///     // Handle Error
        /// }
        ///</code>
        /// </example>
        public SendResponse Send(IBulkMessage message)
        {
            try
            {
                var source = new CancellationTokenSource();

                //Read this if you have questions: https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
                var sendTask = Task.Run(() => SendAsync(message, source.Token));

                while (!sendTask.IsCompleted) { }
                if (sendTask.Status == TaskStatus.Faulted) throw sendTask.Exception;

                return sendTask.Result;
            }
            //for synchronous usage, try to simplify exceptions being thrown
            catch (AggregateException e)
            {
                if(e.InnerException != null)
                    ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Disposing the HttpClient
        /// </summary>
        public void Dispose()
        {
            _httpClient?.Dispose();
        }


    }
}