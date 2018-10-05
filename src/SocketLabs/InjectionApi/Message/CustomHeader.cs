namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// Represents a custom header as a name and value pair.
    /// </summary>
    /// <example>
    /// Using the constructors
    /// <code>
    /// var header1 = new CustomHeader();
    /// header1.Name = "name1";
    /// header1.Value = "value1";
    ///
    /// var header2 = new CustomHeader("name1", "value1");
    /// </code>
    /// Using extension methods
    /// <code>
    /// var headers = new <![CDATA[ List<ICustomHeader> ]]>();
    /// headers.Add("name1", "value1");
    /// headers.Add("name2", "value2");
    /// </code>
    /// </example>
    /// <seealso cref="ICustomHeader"/>
    /// <seealso cref="SocketLabsExtensions"/>
    public class CustomHeader : ICustomHeader
    {
        /// <summary>
        /// Creates a new instance of the CustomHeader class.
        /// </summary>
        /// <example>
        /// <code>
        /// var header1 = new CustomHeader();
        /// header1.Name = "name1";
        /// header1.Value = "value1";
        /// </code>
        /// </example>
        public CustomHeader() { }

        /// <summary>
        /// Creates a new instance of the CustomHeader class and sets the name and value pair.
        /// </summary>
        /// <param name="name">The name of your custom header.</param>
        /// <param name="value">The value for your custom header.</param>
        /// <example>
        /// <code>
        /// var header2 = new CustomHeader("name1", "value1");
        /// </code>
        /// </example>
        public CustomHeader(string name, string value)
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

        /// <summary>
        /// A quick check to ensure that the custom header is valid.
        /// </summary>
        public bool IsValid => !(string.IsNullOrEmpty(Value) || string.IsNullOrEmpty(Name));

        /// <summary>
        /// Returns the custom header as a name-value pair, useful for debugging.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name ?? "null"}: {Value ?? "null"}";
        }
    }
}