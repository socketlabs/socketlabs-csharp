namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// Represents a custom header as a name and value pair.
    /// </summary>
    /// <example>
    /// Using extension methods
    /// <code>
    /// var headers = new <![CDATA[ List<ICustomHeader> ]]>();
    /// headers.Add("name1", "value1");
    /// headers.Add("name2", "value2");
    /// </code>
    /// </example>
    /// <seealso cref="CustomHeader"/>
    /// <seealso cref="SocketLabsExtensions"/>
    public interface ICustomHeader
    {
        /// <summary>
        /// Gets or sets the custom header name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the custom header value.
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// A quick check to ensure that the custom header is valid.
        /// </summary>
        bool IsValid { get; }
    }
}