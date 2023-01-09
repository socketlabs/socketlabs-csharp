namespace SocketLabs.InjectionApi.Message
{

    /// <summary>
    /// Represents a metadata header as a name and value pair.
    /// </summary>
    /// <example>
    /// Using extension methods
    /// <code>
    /// var metadata = new <![CDATA[ List<IMetadata> ]]>();
    /// metadata.Add("name1", "value1");
    /// metadata.Add("name2", "value2");
    /// </code>
    /// </example>
    /// <seealso cref="Metadata"/>
    /// <seealso cref="SocketLabsExtensions"/>
    public interface IMetadata
    {
        /// <summary>
        /// Gets or sets the metadata header name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the metadata header value.
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// A quick check to ensure that the metadata header is valid.
        /// </summary>
        bool IsValid { get; }
    }

}
