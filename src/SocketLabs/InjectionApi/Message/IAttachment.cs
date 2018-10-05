using System.Collections.Generic;

namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// Represents a message attachment in the form of a byte array.
    /// </summary>
    /// <example>
    /// Using extension methods
    /// <code>
    /// var attachmentList = new <c><![CDATA[ List<IAttachment> ]]></c>();
    /// attachmentList.Add(@"c:\bus.png"); 
    /// attachmentList.Add("bus", "image/png", @"c:\bus.png"); 
    /// attachmentList.Add("bus", "image/png", new byte[] { }); 
    /// attachmentList.Add("bus", "image/png", File.OpenRead(@"c:\bus.png"));
    /// 
    /// var attachment = await message.Attachments.AddAsync("bus.png", "image/png", @".\examples\img\bus.png");
    /// </code>
    /// </example>
    /// <seealso cref="Attachment"/>
    /// <seealso cref="SocketLabsExtensions"/>
    public interface IAttachment
    {
        /// <summary>
        /// The BASE64 encoded string containing the contents of an attachment.
        /// </summary>
        byte[] Content { get; set; }

        /// <summary>
        /// When set, used to embed an image within the body of an email message.
        /// </summary>
        string ContentId { get; set; }

        /// <summary>
        /// The MIME type of the attachment.
        /// </summary>
        /// <example>text/plain, image/jpeg, </example>
        string MimeType { get; set; }

        /// <summary>
        /// Name of attachment (displayed in email clients)
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// A list of custom headers added to the attachment.
        /// </summary>
        IList<ICustomHeader> CustomHeaders { get;set; }
    }
}