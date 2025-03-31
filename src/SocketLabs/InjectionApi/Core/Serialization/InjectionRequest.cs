using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace SocketLabs.InjectionApi.Core.Serialization
{
    /// <summary>
    /// Represents a injection request for sending to the Injection Api.
    /// </summary>
    internal class InjectionRequest
    {
        /// <summary>
        /// Your SocketLabs ServerId number.
        /// </summary>
        public readonly int _serverId;

        /// <summary>
        /// Your SocketLabs Injection API key.
        /// </summary>
        public readonly string? _apiKey;

        /// <summary>
        /// Creates a new instance of the <c>InjectionRequest</c> class.
        /// </summary>
        /// <param name="serverId">Your SocketLabs ServerId number.</param>
        /// <param name="apiKey">Your SocketLabs Injection API key.</param>
        public InjectionRequest(int serverId, string? apiKey)
        {
            _serverId = serverId;
            _apiKey = apiKey;
            Messages = new List<MessageJson>();
        }

        /// <summary>
        /// Gets or sets the list of messages to be sent.
        /// </summary>
        public List<MessageJson> Messages { get; set; } = new();

        /// <summary>
        /// Get the InjectionRequest object serialized into a JSON string
        /// </summary>
        /// <returns></returns>
        public StringContent GetAsJson()
        {
            // Don't include properties that are null
            var serialized = JsonConvert.SerializeObject(this,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }
    } 
}
