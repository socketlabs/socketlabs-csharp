using SocketLabs.InjectionApi.Message;

namespace SocketLabs.InjectionApi.Core
{
    internal interface ISendValidator
    {
        /// <summary>
        /// Validate a basic email message before sending to the Injection API.
        /// </summary>
        /// <param name="message">A <c>BasicMessage</c> object to be sent.</param> 
        /// <returns>A <c>SendResponse</c> with the validation results</returns>
        SendResponse ValidateMessage(IBasicMessage message);

        /// <summary>
        /// Validate a bulk email message before sending to the Injection API.
        /// </summary>
        /// <param name="message">A <c>BulkMessage</c> object to be sent.</param>
        /// <returns>A <c>SendResponse</c> with the validation results</returns>
        SendResponse ValidateMessage(IBulkMessage message);

        /// <summary>
        /// Validate the ServerId and Api Key pair prior before sending to the Injection API.
        /// </summary>
        /// <param name="serverId">Your SocketLabs ServerId number.</param>
        /// <param name="apiKey">Your SocketLabs Injection API key.</param>
        /// <returns>A <c>SendResponse</c> with the validation results</returns>
        SendResponse ValidateCredentials(int serverId, string apiKey);
    }
}