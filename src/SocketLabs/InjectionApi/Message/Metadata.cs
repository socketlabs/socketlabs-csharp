namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// Represents a metadata item as a key and value pair.
    /// </summary>
    /// <example>
    /// Using the constructors
    /// <code>
    /// var metadata1 = new Metadata();
    /// metadata1.Key = "key1";
    /// metadata1.Value = "value1";
    ///
    /// var metadata2 = new Metadata("key1", "value1");
    /// </code>
    /// Using extension methods
    /// <code>
    /// var metadata = new <![CDATA[ List<IMetadata> ]]>();
    /// metadata.Add("key1", "value1");
    /// metadata.Add("key2", "value2");
    /// </code>
    /// </example>
    /// <seealso cref="IMetadata"/>
    /// <seealso cref="SocketLabsExtensions"/>
    public class Metadata : IMetadata
    {
        /// <summary>
        /// Creates a new instance of the Metadata class.
        /// </summary>
        /// <example>
        /// <code>
        /// var metadata = new Metadata();
        /// metadata.Name = "key1";
        /// metadata.Value = "value1";
        /// </code>
        /// </example>
        public Metadata() { }

        /// <summary>
        /// Creates a new instance of the Metadata class and sets the name and value pair.
        /// </summary>
        /// <param name="key">The name of your metadata header.</param>
        /// <param name="value">The value for your metadata header.</param>
        /// <example>
        /// <code>
        /// var metadata = new Metadata("name1", "value1");
        /// </code>
        /// </example>
        public Metadata(string key, string value)
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

        /// <summary>
        /// A quick check to ensure that the metadata entry is valid.
        /// </summary>
        public bool IsValid => !(string.IsNullOrEmpty(Value) || string.IsNullOrEmpty(Key));

        /// <summary>
        /// Returns the metadata header as a key-value pair, useful for debugging.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Key ?? "null"}: {Value ?? "null"}";
        }
    }
}