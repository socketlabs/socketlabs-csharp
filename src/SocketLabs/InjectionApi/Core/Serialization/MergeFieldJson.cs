namespace SocketLabs.InjectionApi.Core.Serialization
{
    /// <summary>
    /// Represents a merge field as a field and value pair.
    /// To be serialized into JSON string before sending to the Injection Api.
    /// </summary>
    internal class MergeFieldJson
    {
        /// <summary>
        /// Creates a new instance of the MergeFieldJson class and sets the field and value pair.
        /// </summary>
        /// <param name="field">The field of your merge field.</param>
        /// <param name="value">The value for your merge field.</param>
        public MergeFieldJson(string field, string value)
        {
            Field = field;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the merge field. 
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the merge field value.
        /// </summary>
        public string Value { get; set; }
    }
}