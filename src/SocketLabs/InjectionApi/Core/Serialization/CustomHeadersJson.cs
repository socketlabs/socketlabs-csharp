namespace SocketLabs.InjectionApi.Core.Serialization
{
    /// <summary>
    /// Represents a custom header as a name and value pair.
    /// To be serialized into JSON string before sending to the Injection Api.
    /// </summary>
    internal class CustomHeadersJson
    {
        /// <summary>
        /// Creates a new instance of the CustomHeaderJson class and sets the name and value pair.
        /// </summary>
        /// <param name="name">The name of your custom header.</param>
        /// <param name="value">The value for your custom header.</param>
        public CustomHeadersJson(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the custom header name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the custom header value.
        /// </summary>
        public string Value { get; set; }
    }
}