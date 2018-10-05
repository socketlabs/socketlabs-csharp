using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SocketLabs.InjectionApi.Message;

namespace SocketLabs.InjectionApi 
{
    /// <summary>
    /// Various static extension methods that make it easier to work with the SDK objects in whatever 
    /// coding style and patterns work best.
    /// </summary>
    /// <seealso cref="IAttachment"/>
    /// <seealso cref="IEmailAddress"/>
    /// <seealso cref="IBulkRecipient"/>
    /// <seealso cref="ICustomHeader"/>
    public static class SocketLabsExtensions
    { 
        #region EmailAddress

        /// <summary>
        /// Sets the Email property of an <see cref="IEmailAddress"/> object.
        /// </summary>
        /// <param name="email">The <see cref="IEmailAddress"/> object.</param>
        /// <param name="emailAddress">The new email address being set.</param>
        /// <returns>Instance of <see cref="IEmailAddress"/></returns>
        /// <example>
        /// <code>
        /// var email = new EmailAddress();
        /// email.Set("recipient@example.com");
        /// </code>
        /// </example>
        public static IEmailAddress Set(this IEmailAddress email, string emailAddress)
        {
            email.Email = emailAddress;
            return email;
        }

        /// <summary>
        /// Sets the Email and FriendlyName properties of an <see cref="IEmailAddress"/> object.
        /// </summary>
        /// <param name="email">The <see cref="IEmailAddress"/> object.</param>
        /// <param name="emailAddress">The new email address being set.</param>
        /// <param name="friendlyName">The </param>
        /// <returns>Instance of <see cref="IEmailAddress"/></returns>
        /// <example>
        /// <code>
        /// var email = new EmailAddress();
        /// email.Set("recipient@example.com", "Recipient");
        /// </code>
        /// </example>
        public static IEmailAddress Set(this IEmailAddress email, string emailAddress, string friendlyName)
        {
            email.Email = emailAddress;
            email.FriendlyName = friendlyName;
            return email;
        }

        #endregion

        #region IBulkRecipient

        /// <summary>
        /// Adds merge field and value to a <see cref="IBulkRecipient"/>
        /// </summary>
        /// <param name="recipient">The <see cref="IBulkRecipient"/> object.</param>
        /// <param name="field">merge field name</param>
        /// <param name="value">merge field value</param> 
        /// <returns>Instance of <see cref="IBulkRecipient"/></returns>
        /// <example>
        /// <code> 
        /// var email = new IBulkRecipient("recipient@example.com");
        /// email.AddMergeFields("key1", "value1");
        /// email.AddMergeFields("key2", "value2");
        /// </code>
        /// </example>
        public static IBulkRecipient AddMergeFields(this IBulkRecipient recipient, string field, string value)
        {
            recipient.MergeData.Add(field, value);
            return recipient;
        }

        #endregion

        #region List of BulkRecipients

        /// <summary>
        /// Adds a new bulk recipient to a list of bulk recipients.
        /// </summary>
        /// <param name="source">The existing <see cref="List{IBulkRecipient}"/>.</param>
        /// <param name="emailAddress">The email address for the new recipient.</param>
        /// <param name="friendlyName">The friendly name for the new recipient.</param>
        /// <param name="mergeData">MergeData unique to the new recipient.</param>
        /// <returns>Single Instance of <see cref="IBulkRecipient"/> added to the list.</returns>
        /// <example>
        /// <code>
        /// var emailList = new <![CDATA[ List<IBulkRecipient> ]]>();
        /// 
        /// var mergeData = new <![CDATA[ Dictionary<string, string> ]]>();
        /// mergeData.Add("key1", "value1");
        /// mergeData.Add("key2", "value2");
        /// 
        /// emailList.Add("recipient@example.com", "Recipient", mergeData);
        /// </code>
        /// </example>
        public static IBulkRecipient Add(this IList<IBulkRecipient> source, string emailAddress, string friendlyName, IDictionary<string, string> mergeData)
        {
            var recipient = new BulkRecipient(emailAddress, friendlyName, mergeData);
            source.Add(recipient);
            return recipient;
        }

        /// <summary>
        /// Adds a new bulk recipient to a list of bulk recipients.
        /// </summary>
        /// <param name="source">The existing <see cref="List{IBulkRecipient}"/>.</param>
        /// <param name="emailAddress">The email address for the new recipient.</param>
        /// <param name="mergeData">MergeData unique to the new recipient.</param>
        /// <returns>Single Instance of <see cref="IBulkRecipient"/> added to the list.</returns>
        /// <example>
        /// <code>
        /// var emailList = new <![CDATA[ List<IBulkRecipient> ]]>();
        ///  
        /// var mergeData = new <![CDATA[ Dictionary<string, string> ]]>();
        /// mergeData.Add("key1", "value1");
        /// mergeData.Add("key2", "value2");
        /// 
        /// emailList.Add("recipient@example.com", mergeData);
        /// </code>
        /// </example>
        public static IBulkRecipient Add(this IList<IBulkRecipient> source, string emailAddress, IDictionary<string, string> mergeData)
        {
            return Add(source, emailAddress, null, mergeData);

        }

        /// <summary>
        /// Adds a new bulk recipient to a list of bulk recipients.
        /// </summary>
        /// <param name="source">The existing <see cref="List{IBulkRecipient}"/>.</param>
        /// <param name="emailAddress">The email address for the new recipient.</param>
        /// <param name="friendlyName">The friendly name for the new recipient.</param>
        /// <returns>Single Instance of <see cref="IBulkRecipient"/> added to the list.</returns>
        /// <example>
        /// <code>
        /// var emailList = new <![CDATA[ List<IBulkRecipient> ]]>();
        /// emailList.Add("recipient@example.com", "Recipient");
        /// </code>
        /// </example>
        public static IBulkRecipient Add(this IList<IBulkRecipient> source, string emailAddress, string friendlyName)
        {
            return Add(source, emailAddress, friendlyName, null);
        }

        /// <summary>
        /// Adds a new bulk recipient to a list of bulk recipients.
        /// </summary>
        /// <param name="source">The existing <![CDATA[ List<IBulkRecipient> ]]>.</param>
        /// <param name="emailAddress">The email address for the new recipient.</param>
        /// <returns>Single Instance of <see cref="IBulkRecipient"/> added to the list.</returns>
        /// <example>
        /// <code>
        /// var emailList = new <![CDATA[ List<IBulkRecipient> ]]>(); 
        /// emailList.Add("recipient@example.com");
        /// </code>
        /// </example>
        public static IBulkRecipient Add(this IList<IBulkRecipient> source, string emailAddress)
        {
            return Add(source, emailAddress, (string)null);
        }

        #endregion

        #region List of EmailAddress

        /// <summary>
        /// Adds a new recipient to an existing list of recipients.
        /// </summary>
        /// <param name="source">The existing recipient list.</param>
        /// <param name="emailAddress">The new recipient's email address.</param>
        /// <param name="friendlyName">The new recipient's friendly name.</param>
        /// <returns>Instance of <see cref="IEmailAddress"/></returns>
        /// <example>
        /// <code>
        /// var emailList = new <![CDATA[ List<IEmailAddress> ]]>();
        /// emailList.Add("recipient@example.com", "Recipient");
        /// </code>
        /// </example>
        public static IEmailAddress Add(this IList<IEmailAddress> source, string emailAddress, string friendlyName)
        {
            var recipient = new EmailAddress(emailAddress, friendlyName);
            source.Add(recipient);
            return recipient;
        }

        /// <summary>
        /// Adds a new recipient to an existing list of recipients.
        /// </summary>
        /// <param name="source">The existing recipient list.</param>
        /// <param name="emailAddress">The new recipient's email address.</param>
        /// <returns>Instance of <see cref="IEmailAddress"/></returns>
        /// <example>
        /// <code>
        /// var emailList = new <![CDATA[ List<IEmailAddress> ]]>();
        /// emailList.Add("recipient@example.com");
        /// </code>
        /// </example>
        public static IEmailAddress Add(this IList<IEmailAddress> source, string emailAddress)
        {
            return Add(source, emailAddress, null);
        }

        #endregion

        #region List of Attachments

        /// <summary>
        /// Adds a new attachment to an existing list of attachments
        /// </summary>
        /// <param name="source">The existing attachment list.</param>
        /// <param name="filePath">The path to your attachment on your local system.</param>
        /// <returns>Instance of <see cref="IAttachment"/></returns>
        /// <example>
        /// <code>
        /// var attachments = new <![CDATA[ List<IAttachment> ]]>();
        /// attachments.Add(@"c:\bus.png");
        /// </code>
        /// </example>
        public static IAttachment Add(this IList<IAttachment> source, string filePath)
        {
            var attachment = new Attachment(filePath);
            source.Add(attachment);
            return attachment;
        }

        /// <summary>
        /// Asynchronously adds a new attachment to an existing list of attachments
        /// </summary>
        /// <param name="source">The existing attachment list.</param>
        /// <param name="filePath">The path to your attachment on your local system.</param>
        /// <returns>Instance of <see cref="IAttachment"/></returns>
        /// <example>
        /// <code>
        /// var attachments = new <![CDATA[ List<IAttachment> ]]>();
        /// attachments.AddAsync(@"c:\bus.png");
        /// </code>
        /// </example>
        public static async Task<IAttachment> AddAsync(this IList<IAttachment> source, string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var mimetype = Attachment.GetMimeTypeFromExtension(Path.GetExtension(filePath));
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return await AddAsync(source, fileName, mimetype, stream);
            }
        }
        
        /// <summary>
        /// Adds a new attachment to an existing list of attachments
        /// </summary>
        /// <param name="source">The existing attachment list.</param>
        /// <param name="name">The name for your attachment.</param>
        /// <param name="mimeType">The MIME type for your attachment.</param>
        /// <param name="filePath">The path to your attachment on your local system.</param>
        /// <returns>Instance of <see cref="IAttachment"/></returns>
        /// <example>
        /// <code>
        /// var attachments = new <![CDATA[ List<IAttachment> ]]>();
        /// attachments.Add("bus", "image/png", @"c:\bus.png"); 
        /// </code>
        /// </example>
        public static IAttachment Add(this IList<IAttachment> source, string name, string mimeType, string filePath)
        {
            var attachment = new Attachment(name, mimeType, filePath);
            source.Add(attachment);
            return attachment;
        }

        /// <summary>
        /// Asynchronously adds a new attachment to an existing list of attachments
        /// </summary>
        /// <param name="source">The existing attachment list.</param>
        /// <param name="name">The name for your attachment.</param>
        /// <param name="mimeType">The MIME type for your attachment.</param>
        /// <param name="filePath">The path to your attachment on your local system.</param>
        /// <returns>Instance of <see cref="IAttachment"/></returns>
        /// <example>
        /// <code>
        /// var attachments = new <![CDATA[ List<IAttachment> ]]>();
        /// attachments.AddAsync("bus", "image/png", @"c:\bus.png"); 
        /// </code>
        /// </example>
        public static async Task<IAttachment> AddAsync(this IList<IAttachment> source, string name, string mimeType, string filePath)
        {
            using (var stream = new FileStream(filePath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
            {
                return await AddAsync(source,name,mimeType,stream);
            }
        }

        /// <summary>
        /// Adds a new attachment to an existing list of attachments
        /// </summary>
        /// <param name="source">The existing attachment list.</param>
        /// <param name="name">The name for your attachment.</param>
        /// <param name="mimeType">The MIME type for your attachment.</param>
        /// <param name="content">A byte array containing the attachment content.</param>
        /// <returns>Instance of <see cref="IAttachment"/></returns>
        /// <example>
        /// <code>
        /// var attachments = new <![CDATA[ List<IAttachment> ]]>();
        /// attachments.Add("bus", "image/png", new byte[] { }); 
        /// </code>
        /// </example>
        public static IAttachment Add(this IList<IAttachment> source, string name, string mimeType, byte[] content)
        {
            var attachment = new Attachment(name, mimeType, content);
            source.Add(attachment);
            return attachment;
        }

        /// <summary>
        /// Adds a new attachment to an existing list of attachments
        /// </summary>
        /// <param name="source">The existing attachment list.</param>
        /// <param name="name">The name for your attachment.</param>
        /// <param name="mimeType">The MIME type for your attachment.</param>
        /// <param name="stream">A file stream containing the attachment content.</param>
        /// <returns>Instance of <see cref="IAttachment"/></returns>
        /// <example>
        /// <code>
        /// var attachments = new <![CDATA[ List<IAttachment> ]]>();
        /// attachments.Add("bus", "image/png", File.OpenRead(@"c:\bus.png"));
        /// </code>
        /// </example>
        public static IAttachment Add(this IList<IAttachment> source, string name, string mimeType, Stream stream)
        {
            var attachment = new Attachment(name, mimeType, stream);
            source.Add(attachment);
            return attachment;
        }
        
        /// <summary>
        /// Asynchronously adds a new attachment to an existing list of attachments
        /// </summary>
        /// <param name="source">The existing attachment list.</param>
        /// <param name="name">The name for your attachment.</param>
        /// <param name="mimeType">The MIME type for your attachment.</param>
        /// <param name="stream">A file stream containing the attachment content.</param>
        /// <returns>Instance of <see cref="IAttachment"/></returns>
        /// <example>
        /// <code>
        /// var attachments = new <![CDATA[ List<IAttachment> ]]>();
        /// attachments.AddAsync("bus", "image/png", File.OpenRead(@"c:\bus.png"));
        /// </code>
        /// </example>
        public static async Task<IAttachment> AddAsync(this IList<IAttachment> source, string name, string mimeType, Stream stream)
        {
            byte[] content;

            if (stream is MemoryStream externalMemoryStream)
                content = externalMemoryStream.ToArray();
            else
            {
                using (var temporaryMemoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(temporaryMemoryStream);
                    content = temporaryMemoryStream.ToArray();
                }
            }

            return Add(source, name, mimeType, content);
        }

        #endregion

        #region List of CustomHeaders

        /// <summary>
        /// Adds a new custom header to an existing list of custom headers.
        /// </summary>
        /// <param name="source">The existing list of custom headers.</param>
        /// <param name="name">The name of the new custom header.</param>
        /// <param name="value">The value for the new custom header.</param>
        /// <returns>Instance of <see cref="ICustomHeader"/></returns>
        /// <example>
        /// <code>
        /// var headers = new <![CDATA[ List<ICustomHeader> ]]>();
        /// headers.Add("name1", "value1");
        /// headers.Add("name2", "value2");
        /// </code>
        /// </example>
        public static ICustomHeader Add(this IList<ICustomHeader> source, string name, string value)
        {
            var customHeader = new CustomHeader(name, value);
            source.Add(customHeader);
            return customHeader;
        }

        #endregion
    }
}
