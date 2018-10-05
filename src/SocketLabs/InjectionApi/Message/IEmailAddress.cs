namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// Represents an individual email address for a message.
    /// </summary>
    /// <example> 
    /// Using extension methods available for an <c><![CDATA[ IList<IEmailAddress> ]]></c>
    /// <code>
    /// var emailList = new <![CDATA[ List<IEmailAddress> ]]>();
    /// emailList.Add("recipient1@example.com");
    /// emailList.Add("recipient2@example.com", "Recipient #2");
    /// emailList.Add(new EmailAddress("recipient3@example.com"));
    /// emailList.Add(new EmailAddress("recipient4@example.com", "Recipient #4"));
    /// </code>
    /// </example>
    /// <seealso cref="EmailAddress"/>
    /// <seealso cref="SocketLabsExtensions"/>
    public interface IEmailAddress
    {
        /// <summary>
        /// A valid email address
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// The friendly or display name for the recipient.
        /// </summary>
        string FriendlyName { get; set; }

        /// <summary>
        /// Determines if the Email Address is valid.
        /// </summary>
        /// <returns>True if valid and false if not.</returns>
        /// <remarks>
        /// Does simple syntax validation on the address.
        /// </remarks>
        bool IsValid { get; }
    }
}