using System.Collections.Generic;

namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// Represents an individual recipient for a message including MergeData for a recipient.
    /// </summary>
    /// <example>
    /// Using extension methods available for an <c><![CDATA[ IList<IBulkRecipient> ]]></c>
    /// <code>
    /// var emailList = new <![CDATA[ List<IBulkRecipient> ]]>();
    /// 
    /// var email1 = emailList.Add("recipient1@example.com");
    /// email1.MergeData.Add("key1", "value1");
    /// email1.MergeData.Add("key2", "value2");
    /// 
    /// var email2 = emailList.Add("recipient2@example.com", "Recipient #2");
    /// email2.AddMergeFields("key1", "value1");
    /// email2.AddMergeFields("key2", "value2");
    /// 
    /// var mergeDataFor3 = new <![CDATA[ Dictionary<string, string> ]]>();
    /// mergeDataFor3.Add("key1", "value1");
    /// mergeDataFor3.Add("key2", "value2");  
    /// var email3 = emailList.Add("recipient3@example.com", mergeDataFor3); 
    /// 
    /// var mergeDataFor4 = new <![CDATA[ Dictionary<string, string> ]]>();
    /// mergeDataFor4.Add("key1", "value1");
    /// mergeDataFor4.Add("key2", "value2");  
    /// var email4 = emailList.Add("recipient4@example.com", "Recipient #4", mergeDataFor4);
    /// </code>
    /// </example>
    /// <seealso cref="BulkRecipient"/>
    /// <seealso cref="SocketLabsExtensions"/>
    public interface IBulkRecipient 
    {
        /// <summary>
        /// A valid email address
        /// </summary>
        string Email { get; }

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

        /// <summary>
        /// A dictionary containing MergeData items unique to the recipient.
        /// </summary>
        IDictionary<string, string> MergeData { get; set; }
    }
}