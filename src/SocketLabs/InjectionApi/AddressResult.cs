namespace SocketLabs.InjectionApi
{
    /// <summary>
    /// The result of a single recipient in the Injection request.
    /// </summary>
    public class AddressResult
    {
        /// <summary>
        /// The recipient's email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Whether the recipient was accepted for delivery.
        /// </summary>
        public bool Accepted { get; set; }

        /// <summary>
        /// An error code detailing why the recipient was not accepted.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Represents the <c>AddressResult</c> as a string.  Useful for debugging.
        /// </summary>
        public override string ToString()
        {
            return $"{ErrorCode}: {EmailAddress}";
        }
    }
}