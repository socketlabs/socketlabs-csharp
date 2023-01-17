namespace SocketLabs.InjectionApi.Core.Serialization
{
    /// <summary>
    /// Represents a metadata item as a key and value pair.
    /// To be serialized into JSON string before sending to the Injection Api.
    /// </summary>
    internal class MetadataHeaderJson
    {
        /// <summary>
        /// Creates a new instance of the MetadataHeaderJson class and sets the key and value pair.
        /// </summary>
        /// <param name="key">The key of your custom header.</param>
        /// <param name="value">The value for your custom header.</param>
        public MetadataHeaderJson(string key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the metadata key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the metadata value.
        /// </summary>
        public string Value { get; set; }
    }
}