namespace SocketLabs.InjectionApi.Core.Serialization
{
    /// <summary>
    /// Data transfer object representing a message result from the Injection Api.
    /// </summary>
    internal class MessageResultDto
    {
        /// <summary>
        /// Index of message being sent. 
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The resulting response ErrorCode of the Injection Api send request
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// An array of AddressResult objects that contain the status of each address that failed. If no messages failed this array is empty.
        /// </summary>
        public AddressResult[] AddressResults { get; set; }
    }
}
