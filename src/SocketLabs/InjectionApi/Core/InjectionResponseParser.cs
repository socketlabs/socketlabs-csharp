using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocketLabs.InjectionApi.Core.Serialization;

namespace SocketLabs.InjectionApi.Core
{
    /// <summary>
    /// Used by the <c>SocketLabsClient</c> to convert the response form the Injection API.
    /// </summary>
    internal class InjectionResponseParser
    {
        /// <summary>
        /// Parse the response from theInjection Api into <c>SendResponse</c>
        /// </summary>
        /// <param name="httpResponse">The <c>HttpResponseMessage</c> from the Injection Api</param>
        /// <returns>A <c>SendResponse</c> from the Injection Api response</returns>
        public SendResponse Parse(HttpResponseMessage httpResponse)
        {
            //Read this if you have questions: https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
            var contentString = Task.Run(() => httpResponse.Content.ReadAsStringAsync()).Result;

            var injectionResponse = JsonConvert.DeserializeObject<InjectionResponseDto>(contentString);

            var resultEnum = DetermineSendResult(injectionResponse, httpResponse);
            var newResponse = new SendResponse
            {
                Result = resultEnum,
                TransactionReceipt = injectionResponse.TransactionReceipt
            };

            if (resultEnum == SendResult.Warning && (injectionResponse.MessageResults != null && injectionResponse.MessageResults.Length > 0))
            {
                System.Enum.TryParse(injectionResponse.MessageResults[0].ErrorCode, true, out SendResult errorCode);
                newResponse.Result = errorCode;
            }
            
            if (injectionResponse.MessageResults != null && injectionResponse.MessageResults.Length > 0)
                newResponse.AddressResults = injectionResponse.MessageResults[0].AddressResults;
 
            return newResponse;
        }

        /// <summary>
        /// Enumerated SendResult of the payload response from the Injection Api
        /// </summary>
        /// <param name="responseDto">The <c>InjectionResponseDto</c> from the Injection Api</param>
        /// <param name="httpResponse">The <c>HttpResponseMessage</c> from the Injection Api</param>
        /// <returns>The <c>SendResult</c> from the Injection Api response</returns>
        internal virtual SendResult DetermineSendResult(InjectionResponseDto responseDto, HttpResponseMessage httpResponse)
        {
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    if (!System.Enum.TryParse(responseDto.ErrorCode, true, out SendResult errorCode))

                        return SendResult.UnknownError;
                    return errorCode;

                case HttpStatusCode.InternalServerError:
                    return SendResult.InternalError;

                case HttpStatusCode.RequestTimeout:
                    return SendResult.Timeout;

                case HttpStatusCode.Unauthorized:
                    return SendResult.InvalidAuthentication;

                default:
                    return SendResult.UnknownError;
            } 
        }
    }
}
