using System.Collections.Generic;

namespace SocketLabs.InjectionApi.Core.Serialization
{
    /// <summary>
    /// Represents a message attachment in the form of a byte array.
    /// To be serialized into JSON string before sending to the Injection Api.
    /// </summary>
    internal class AttachmentJson
    {
        /// <summary>
        /// Name of attachment (displayed in email clients)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The BASE64 encoded string containing the contents of an attachment.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// When set, used to embed an image within the body of an email message.
        /// </summary>
        public string ContentId { get; set; }

        /// <summary>
        /// The ContentType (MIME type) of the attachment.
        /// </summary>
        /// <example>text/plain, image/jpeg, </example>
        public string ContentType { get; set; }

        /// <summary>
        /// A list of custom headers added to the attachment.
        /// </summary>
        public List<CustomHeadersJson> CustomHeaders { get; set; }
    }
}