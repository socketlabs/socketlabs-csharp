namespace SocketLabs.InjectionApi.Core.Serialization
{
    /// <summary>
    /// Represents an individual email address for a message.
    /// To be serialized into JSON string before sending to the Injection Api.
    /// </summary>
    internal class AddressJson
    {
        /// <summary>
        /// Creates a new instance of the AddressJson class and sets the email address.
        /// </summary>
        /// <param name="emailAddress">A valid email address</param>
        /// <param name="friendlyName">The friendly or display name for the recipient.</param>
        public AddressJson(string emailAddress, string friendlyName = null)
        {
            EmailAddress = emailAddress;
            FriendlyName = friendlyName;
        }

        /// <summary>
        /// A valid email address
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The friendly or display name for the recipient.
        /// </summary>
        public string FriendlyName { get; set; }
    }
}