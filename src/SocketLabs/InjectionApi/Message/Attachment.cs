using System.Collections.Generic;
using System.IO;

namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// Represents a message attachment in the form of a byte array.
    /// </summary>
    /// <example>
    /// Using the constructors
    /// <code>
    /// var attachment1 = new Attachment(@"c:\bus.png"); 
    /// var attachment2 = new Attachment("bus", "image/png", @"c:\bus.png"); 
    /// var attachment3 = new Attachment("bus", "image/png", new byte[] { }); 
    /// var attachment4 = new Attachment("bus", "image/png", File.OpenRead(@"c:\bus.png"));
    /// </code>
    /// 
    /// Adding CustomHeaders (<see cref="CustomHeader"/>) to an attachment
    /// <code>
    /// var attachment = new Attachment(@"c:\bus.png"); 
    /// attachment.CustomHeaders.Add(new CustomHeader("Color", "Orange"));
    /// attachment.CustomHeaders.Add(new CustomHeader("Place", "Beach"));
    /// </code>
    /// </example>
    /// <seealso cref="IAttachment"/>
    /// <seealso cref="SocketLabsExtensions"/>
    public class Attachment : IAttachment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment "/> class
        /// </summary>
        /// <example>
        /// <code>
        /// var attachment = new Attachment();
        /// attachment.Name = Path.GetFileName(@"c:\bus.png");
        /// attachment.Content = File.ReadAllBytes(@"c:\bus.png");
        /// attachment.MimeType = "image/png";
        /// </code>
        /// </example>
        public Attachment() : this(null, null, (byte[]) null) { }


        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment "/> class
        /// </summary>
        /// <param name="filePath">The path to your attachment on your local system.</param>
        /// <example>
        /// <code>
        /// var attachment1 = new Attachment(@"c:\bus.png"); 
        /// </code>
        /// </example>
        public Attachment(string filePath)
        {
            Name = Path.GetFileName(filePath);
            MimeType = GetMimeTypeFromExtension(Path.GetExtension(filePath));

            Content = File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment "/> class
        /// </summary>
        /// <param name="name">The name for your attachment.</param>
        /// <param name="mimeType">The MIME type for your attachment.</param>
        /// <param name="filePath">The path to your attachment on your local system.</param>
        /// <example>
        /// <code>
        /// var attachment = new Attachment("bus", "image/png", @"c:\bus.png"); 
        /// </code>
        /// </example>
        public Attachment(string name, string mimeType, string filePath)
        {
            Name = name;
            MimeType = mimeType;

            Content = File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment "/> class
        /// </summary>
        /// <param name="name">The name for your attachment.</param>
        /// <param name="mimeType">The MIME type for your attachment.</param>
        /// <param name="content">A byte array containing the attachment content.</param>
        /// <example>
        /// <code>
        /// var attachment = new Attachment("bus", "image/png", new byte[] { }); 
        /// </code>
        /// </example>
        public Attachment(string name, string mimeType, byte[] content)
        {
            Name = name;
            MimeType = mimeType;
            Content = content;
        }

        /// <summary>
        /// Creates a new instance of the Attachment class.
        /// </summary>
        /// <param name="name">The name for your attachment.</param>
        /// <param name="mimeType">The MIME type for your attachment.</param>
        /// <param name="stream">A file stream containing the attachment content.</param>
        /// <example>
        /// <code>
        /// var attachment = new Attachment("bus", "image/png", File.OpenRead(@"c:\bus.png"));
        /// </code>
        /// </example>
        public Attachment(string name, string mimeType, Stream stream)
        {
            Name = name;
            MimeType = mimeType;

            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                Content = ms.ToArray();
            }
        }

        /// <summary>
        /// Name of attachment (displayed in email clients)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The MIME type of the attachment.
        /// </summary>
        /// <example>text/plain, image/jpeg, </example>
        public string MimeType { get; set; }

        /// <summary>
        /// When set, used to embed an image within the body of an email message.
        /// </summary>
        public string ContentId { get; set; }

        /// <summary>
        /// The byte array containing the contents of an attachment.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// A list of custom headers added to the attachment.
        /// </summary>
        public IList<ICustomHeader> CustomHeaders { get; set; } = new List<ICustomHeader>();

        /// <summary>
        /// Converts the file extension to the correct mime type. This is a small subset of more common used formats.
        /// </summary>
        /// <param name="extension">file extension of the attachment</param>
        internal static string GetMimeTypeFromExtension(string extension)
        {
            switch (extension)
            {
                case ".xml":
                    return Message.MimeType.XML;
                case ".txt":
                case ".ini":
                case ".sln":
                case ".cs":
                case ".js":
                case ".config":
                case ".vb":
                    return Message.MimeType.TEXT;
                case ".html":
                    return Message.MimeType.HTML;
                case ".wav":
                    return "audio/wav";
                case ".eml":
                    return "message/rfc822";
                case ".mp3":
                    return "audio/mpeg";
                case ".mp4":
                    return "video/mp4";
                case ".bmp":
                    return Message.MimeType.BMP;
                case ".gif":
                    return Message.MimeType.GIF;
                case ".jpeg":
                case ".jpg":
                    return Message.MimeType.JPEG;
                case ".png":
                    return Message.MimeType.PNG;
                case ".zip":
                        return Message.MimeType.ZIP;
                case ".doc":
                    return Message.MimeType.DOC;
                case ".docx":
                    return Message.MimeType.DOCX;
                case ".xls":
                    return Message.MimeType.XLS;
                case ".xlsx":
                    return Message.MimeType.XLSX;
                case ".ppt":
                    return Message.MimeType.PPT;
                case ".pptx":
                    return Message.MimeType.PPTX;
                case ".csv":
                    return Message.MimeType.CSV;
                case ".pdf":
                    return Message.MimeType.PDF;
                case ".mov":
                    return "video/quicktime";

                default:
                    return "application/octet-stream";
            }
        }

        /// <summary>
        /// Represents the attachment by name and mime type, useful for debugging.
        /// </summary>
        public override string ToString()
        {
            return $"{Name}, {MimeType}";
        }
    }
}
