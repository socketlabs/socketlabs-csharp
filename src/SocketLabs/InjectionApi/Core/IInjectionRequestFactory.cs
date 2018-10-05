using SocketLabs.InjectionApi.Core.Serialization;
using SocketLabs.InjectionApi.Message;

namespace SocketLabs.InjectionApi.Core
{
    internal interface IInjectionRequestFactory
    {
        /// <summary>
        /// Generate the <c>InjectionRequest</c> for sending to the Injection Api
        /// </summary>
        /// <param name="message">An <c>IBasicMessage</c> object to be sent.</param> 
        /// <returns>An <c>InjectionRequest</c> for sending to the Injection Api</returns>
        InjectionRequest GenerateRequest(IBasicMessage message);

        /// <summary>
        /// Generate the <c>InjectionRequest</c> for sending to the Injection Api
        /// </summary>
        /// <param name="message">An <c>IBulkMessage</c> object to be sent.</param>
        /// <returns>An <c>InjectionRequest</c> for sending to the Injection Api</returns>
        InjectionRequest GenerateRequest(IBulkMessage message);
    }
}