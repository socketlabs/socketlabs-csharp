using System.Collections.Generic;
using Newtonsoft.Json;

namespace SocketLabs.InjectionApi.Core.Serialization
{
    /// <summary>
    /// Represents a message for sending to the Injection Api.
    /// To be serialized into JSON string before sending to the Injection Api.
    /// </summary>
    internal class MessageJson
    {
        /// <summary>
        /// Creates a new instance of the MessageJson class.
        /// </summary>
        public MessageJson()
        {
            To = new List<AddressJson>();
            Cc = new List<AddressJson>();
            Bcc = new List<AddressJson>();
            MergeData = new MergeDataJson();
            Attachments = new List<AttachmentJson>();
        }

        /// <summary>
        /// Gets or sets the list of To recipients.
        /// </summary> 
        [JsonProperty("To", NullValueHandling = NullValueHandling.Ignore)]
        public List<AddressJson> To { get; set; }

        /// <summary>
        /// Gets or sets the From address.
        /// </summary>
        public AddressJson From { get; set; }

        /// <summary>
        /// Gets or sets the instance of the message Subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the plain text portion of the message body.        
        /// </summary>
        public string TextBody { get; set; }

        /// <summary>
        /// Gets or sets the HTML portion of the message body.
        /// </summary>
        public string HtmlBody { get; set; }

        /// <summary>
        /// Gets or sets the AMP portion of the message body.
        /// </summary>
        public string AmpBody { get; set; }

        /// <summary>
        /// Gets or sets the Api Template for the message.
        /// </summary>
        public string ApiTemplate { get; set; }

        /// <summary>
        /// Gets or sets the custom MailingId for the message.
        /// </summary>
        public string MailingId { get; set; }

        /// <summary>
        /// Gets or sets the custom MessageId for the message.
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// The optional character set for your message.
        /// </summary>
        public string CharSet { get; set; }

        /// <summary>
        /// A list of custom message headers added to the message.
        /// </summary>
        public List<CustomHeadersJson> CustomHeaders { get; set; }
        
        /// <summary>
        /// Gets or sets the list of CC recipients.
        /// </summary>
        public List<AddressJson> Cc { get; set; }

        /// <summary>
        /// Gets or sets the list of BCC recipients.
        /// </summary>
        public List<AddressJson> Bcc { get; set; }
        
        /// <summary>
        /// Gets or sets the Reply To address.
        /// </summary>
        public AddressJson ReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the list of attachments.
        /// </summary>
        public List<AttachmentJson> Attachments { get; set; }

        /// <summary>
        /// Gets or sets the list of merge data.
        /// </summary>
        public MergeDataJson MergeData { get; set; }

        #region Conditional Property Serialization

        /// <summary>
        /// Check if ro recipients should be serialized.
        /// </summary>
        /// <remarks>
        /// Don't serialize the collection if they are null or empty.
        /// https://www.newtonsoft.com/json/help/html/ConditionalProperties.htm
        /// </remarks>
        /// <returns><c>bool</c> result</returns>
        public bool ShouldSerializeTo()
        {
            return To == null || To.Count > 0;
        }

        /// <summary>
        /// Check if cc recipients should be serialized.
        /// </summary>
        /// <remarks>
        /// Don't serialize the collection if they are null or empty.
        /// https://www.newtonsoft.com/json/help/html/ConditionalProperties.htm
        /// </remarks>
        /// <returns><c>bool</c> result</returns>
        public bool ShouldSerializeCc()
        {
            return Cc == null || Cc.Count > 0;
        }

        /// <summary>
        /// Check if bcc recipients should be serialized.
        /// </summary>
        /// <remarks>
        /// Don't serialize the collection if they are null or empty.
        /// https://www.newtonsoft.com/json/help/html/ConditionalProperties.htm
        /// </remarks>
        /// <returns><c>bool</c> result</returns>
        public bool ShouldSerializeBcc()
        {
            return Bcc == null || Bcc.Count > 0;
        }

        /// <summary>
        /// Check if merge data should be serialized.
        /// </summary>
        /// <remarks>
        /// Don't serialize the collection if they are null or empty.
        /// https://www.newtonsoft.com/json/help/html/ConditionalProperties.htm
        /// </remarks>
        /// <returns><c>bool</c> result</returns>
        public bool ShouldSerializeMergeData()
        {
            return MergeData?.Global?.Count > 0 || MergeData?.PerMessage?.Count > 0;
        }

        /// <summary>
        /// Check if attachments should be serialized.
        /// </summary>
        /// <remarks>
        /// Don't serialize the collection if they are null or empty.
        /// https://www.newtonsoft.com/json/help/html/ConditionalProperties.htm
        /// </remarks>
        /// <returns><c>bool</c> result</returns>
        public bool ShouldSerializeAttachment()
        {
            return Attachments == null || Attachments.Count > 0;
        }

        #endregion
    }
}