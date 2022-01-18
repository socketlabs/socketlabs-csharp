using System.Threading;
using System.Threading.Tasks;
using SocketLabs.InjectionApi.Message;

namespace SocketLabs.InjectionApi
{
    /// <summary>
    /// ISocketLabs client is an interface that defines the SocketLabs Injection API client
    /// and its public methods.
    /// </summary>
    public interface ISocketLabsClient
    {

        /// <summary>
        /// Asynchronously sends a basic email message and returns the response from the Injection API.
        /// </summary>
        /// <param name="message">A <c>BasicMessage</c> object to be sent.</param> 
        /// <param name="cancellationToken">A <c>CancellationToken</c> to handle cancellation between async threads.</param> 
        /// <returns>A <c>SendResponse</c> of an SocketLabsClient send request.</returns>
        Task<SendResponse> SendAsync(IBasicMessage message, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously sends a bulk email message and returns the response from the Injection API.
        /// </summary>
        /// <param name="message">A <c>BulkMessage</c> object to be sent.</param>
        /// <param name="cancellationToken">A <c>CancellationToken</c> to handle cancellation between async threads.</param> 
        /// <returns>A <c>SendResponse</c> of an SocketLabsClient send request.</returns>
        Task<SendResponse> SendAsync(IBulkMessage message, CancellationToken cancellationToken);

        /// <summary>
        /// Synchronously sends a basic email message and returns the response from the Injection API.
        /// </summary>
        /// <param name="message">A <c>BasicMessage</c> object to be sent.</param> 
        /// <returns>A <c>SendResponse</c> of an SocketLabsClient send request.</returns>
        SendResponse Send(IBasicMessage message);

        /// <summary>
        /// Synchronously sends a bulk email message and returns the response from the Injection API.
        /// </summary>
        /// <param name="message">A <c>BulkMessage</c> object to be sent.</param>
        /// <returns>A <c>SendResponse</c> of an SocketLabsClient send request.</returns>
        SendResponse Send(IBulkMessage message);
    }
}