namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// Represents an individual email address for a message.
    /// </summary>
    /// <example>
    /// Using the constructors
    /// <code>
    /// var email1 = new EmailAddress("recipient1@example.com");
    /// var email2 = new EmailAddress("recipient2@example.com", "Recipient #2");
    /// </code>
    /// Using extension methods
    /// <code>
    /// var email3 = new EmailAddress();
    /// email3.Set("recipient1@example.com");
    /// 
    /// var email4 = new EmailAddress();
    /// email4.Set("recipient4@example.com", "Recipient #4");
    /// </code>
    /// </example>
    /// <seealso cref="IEmailAddress"/>
    /// <seealso cref="SocketLabsExtensions"/>
    public class EmailAddress : IEmailAddress
    {
        /// <summary>
        /// Creates a new instance of the <see cref="IEmailAddress"/> class.
        /// </summary>
        /// <example>
        /// Using the constructors
        /// <code>
        /// var email = new EmailAddress();
        /// </code> 
        /// </example>
        public EmailAddress() : this(null, null) { }

        /// <summary>
        /// Creates a new instance of the <see cref="IEmailAddress"/> class and sets the email address.
        /// </summary>
        /// <param name="email">A valid email address</param>
        /// <example>
        /// Using the constructors
        /// <code>
        /// var email = new EmailAddress("recipient@example.com");
        /// </code>
        /// </example>
        public EmailAddress(string email):this(email, null) { }

        /// <summary>
        /// Creates a new instance of the email address class and sets the email address and the friendly name.
        /// </summary>
        /// <param name="email">A valid email address</param>
        /// <param name="friendlyName">The friendly or display name for the recipient.</param>
        /// <example>
        /// Using the constructors
        /// <code>
        /// var email = new EmailAddress("recipient@example.com", "Recipient");
        /// </code>
        /// </example>
        public EmailAddress(string email, string friendlyName)
        {
            Email = email;
            FriendlyName = friendlyName;
        }

        /// <summary>
        /// A valid email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The friendly or display name for the recipient.
        /// </summary>
        public string FriendlyName { get; set; }

        internal readonly char[] BadEmailCharacters =  { ',', ' ', ';', (char)191 };

        /// <summary>
        /// Determines if the Email Address is valid.
        /// </summary>
        /// <returns>True if valid and false if not.</returns>
        /// <remarks>
        /// Does simple syntax validation on the address.
        /// </remarks>
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Email))
                    return false;

                var parts = Email.Split('@');

                if (parts.Length != 2)
                    return false;
                //320 used over 256 to be more lenient
                if (Email.Length > 320)
                    return false;

                if (parts[0].Trim().Length < 1)
                    return false;
                if (parts[1].Trim().Length < 1)
                    return false;

                return Email.IndexOfAny(BadEmailCharacters) <= -1;
            }
        }

        /// <summary>
        /// Represents the email address as a string similar to how it would look in an email client.  Useful for debugging.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(FriendlyName))
                return $"{FriendlyName} <{Email}>";

            return Email;
        }
    }
}