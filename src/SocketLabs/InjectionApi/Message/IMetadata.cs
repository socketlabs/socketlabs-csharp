namespace SocketLabs.InjectionApi.Message
{

    /// <summary>
    /// Represents a metadata item as a key and value pair.
    /// </summary>
    /// <example>
    /// Using extension methods
    /// <code>
    /// var metadata = new <![CDATA[ List<IMetadata> ]]>();
    /// metadata.Add("key1", "value1");
    /// metadata.Add("key2", "value2");
    /// </code>
    /// </example>
    /// <seealso cref="Metadata"/>
    /// <seealso cref="SocketLabsExtensions"/>
    public interface IMetadata
    {
        /// <summary>
        /// Gets or sets the metadata key.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Gets or sets the metadata value.
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// A quick check to ensure that the metadata item is valid.
        /// </summary>
        bool IsValid { get; }
    }

}
