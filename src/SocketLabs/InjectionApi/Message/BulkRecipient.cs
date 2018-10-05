using System;
using System.Collections.Generic;

namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// Represents an individual recipient for a message.
    /// </summary>
    /// <example>
    /// Using the constructors
    /// <code>
    /// var email1 = new BulkRecipient("recipient1@example.com");
    /// email1.MergeData.Add("key1", "value1");
    /// email1.MergeData.Add("key2", "value2");
    ///
    /// var mergeDataFor2 = new <![CDATA[ Dictionary<string, string> ]]>();
    /// mergeDataFor2.Add("key1", "value1");
    /// mergeDataFor2.Add("key2", "value2"); 
    /// var email2 = new BulkRecipient("recipient2@example.com", "Recipient #2", mergeDataFor2);
    /// 
    /// var email3 = new BulkRecipient("recipient3@example.com", "Recipient #3");
    /// email3.AddMergeFields("key1", "value1");
    /// email3.AddMergeFields("key2", "value2");
    /// 
    /// </code>
    /// Using extension methods
    /// <code>
    /// var email3 = new BulkRecipient();
    /// email3.Set("recipient1@example.com");
    /// 
    /// var email4 = new BulkRecipient();
    /// email4.Set("recipient4@example.com", "Recipient #4");
    /// </code>
    /// </example>
    /// <seealso cref="IBulkRecipient"/>
    /// <seealso cref="SocketLabsExtensions"/>
    public class BulkRecipient : IBulkRecipient
    {
        /// <summary>
        /// Creates a new instance of the BulkRecipient class and sets the email address.
        /// </summary>
        /// <param name="email">The email address for your bulk recipient.</param>
        /// <example>
        ///<code> 
        /// var email = new BulkRecipient("recipient@example.com");
        /// </code>
        /// </example>
        public BulkRecipient(string email) : this(email, null, null) { }

        /// <summary>
        /// Creates a new instance of the BulkRecipient class and sets the email address and friendly name.
        /// </summary>
        /// <param name="email">The email address for your bulk recipient.</param>
        /// <param name="friendlyName">The friendly name for your bulk recipient.</param>
        /// <example>
        ///<code> 
        /// var email = new BulkRecipient("recipient@example.com", "Recipient");
        /// </code>
        /// </example>
        public BulkRecipient(string email, string friendlyName):this(email, friendlyName, null) { }

        /// <summary>
        /// Creates a new instance of the BulkRecipient class and sets the email address and merge data properties.. 
        /// </summary>
        /// <param name="email">The email address for your bulk recipient.</param>
        /// <param name="mergeData">Merge data unique to the instance of the bulk recipient.</param>
        /// <example>
        ///<code>
        /// var mergeData = new <![CDATA[ Dictionary<string, string> ]]>();
        /// mergeData.Add("key1", "value1");
        /// mergeData.Add("key2", "value2");
        /// 
        /// var email = new BulkRecipient("recipient@example.com", mergeData);
        /// </code>
        /// </example>
        public BulkRecipient(string email, IDictionary<string, string> mergeData)
        {
            Email = email;
            FriendlyName = null;
            MergeData = new Dictionary<string, string>(mergeData ?? new Dictionary<string, string>(), StringComparer.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Creates a new instance of the BulkRecipient class and sets the email address, friendly name and merge data properties.. 
        /// </summary>
        /// <param name="email">The email address for your bulk recipient.</param>
        /// <param name="friendlyName">The friendly name for your bulk recipient.</param>
        /// <param name="mergeData">Merge data unique to the instance of the bulk recipient.</param>
        /// <example>
        ///<code>
        /// var mergeData = new <![CDATA[ Dictionary<string, string> ]]>();
        /// mergeData.Add("key1", "value1");
        /// mergeData.Add("key2", "value2");
        /// 
        /// var email = new BulkRecipient("recipient@example.com", "Recipient", mergeData);
        /// </code>
        /// </example>
        public BulkRecipient(string email, string friendlyName, IDictionary<string, string> mergeData)
        { 
            Email = email;
            FriendlyName = friendlyName;
            MergeData = new Dictionary<string, string>(mergeData ?? new Dictionary<string, string>(),  StringComparer.CurrentCultureIgnoreCase ); 
        }


        /// <summary>
        /// A valid email address
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// The friendly or display name for the recipient.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// A dictionary containing MergeData items unique to the recipient.
        /// </summary>
        public IDictionary<string, string> MergeData { get; set; }

        internal readonly char[] BadEmailCharacters = { ',', ' ', ';', (char)191 };

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

                //320 used over 256 to be more liberal
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
