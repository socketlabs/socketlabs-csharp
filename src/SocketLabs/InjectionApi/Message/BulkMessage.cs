using System;
using System.Collections.Generic;

namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// A bulk message usually contains a single recipient per message 
    /// and is generally used to send the same content to many recipients, 
    /// optionally customizing the message via the use of MergeData.
    /// </summary>
    /// <example>
    /// <code>
    /// var message = new BulkMessage();
    ///
    /// message.PlainTextBody = "This is the body of my message sent to ##Name##";
    /// message.HtmlBody = "<![CDATA[ <html> ]]>This is the HtmlBody of my message sent to ##Name##<![CDATA[ </html> ]]>";
    /// message.AmpBody("<!doctype html>" +
            ///"<html amp4email>" +
            ///"<head>" +
            ///"  <meta charset=\"utf-8\">" +
            ///"  <script async src=\"https://cdn.ampproject.org/v0.js\"></script>" +
            ///"  <style amp4email-boilerplate>body{visibility:hidden}</style>" +
            ///"  <style amp-custom>" +
            ///"    h1 {" +
            ///"      margin: 1rem;" +
            ///"    }" +
            ///"  </style>" +
            ///"</head>" +
            ///"<body>" +
            ///"  <h1>This is the AMP Html Body of my message</h1>" +
            ///"</body>" +
            ///"</html>");
    /// message.Subject = "Sending a test message";
    /// message.From.Email = "from@example.com";
    ///
    /// var email1 = new BulkRecipient("recipient1@example.com");
    /// message.To.Add(email1);
    /// 
    /// var email2 = new BulkRecipient("recipient2@example.com", "Recipient #2");
    /// message.To.Add(email2);
    ///
    /// var globalMergeData = new Dictionary<![CDATA[ <string, string> ]]>();
    /// 
    /// globalMergeData.Add("name1", "value1");
    /// globalMergeData.Add("name2", "value2");
    ///
    /// message.GlobalMergeData = globalMergeData;
    ///</code>
    /// </example>
    public class BulkMessage : IBulkMessage
    {
        /// <summary>
        /// Gets or sets the instance of the message Subject.
        /// </summary>
        /// <remarks>
        /// (Required )
        /// </remarks> 
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the plain text portion of the message body.        
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// Either PlainTextBody or HtmlBody must be used or use a ApiTemplate with the AmpBody 
        /// </remarks> 
        public string PlainTextBody { get; set; }

        /// <summary>
        /// Gets or sets the HTML portion of the message body.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// Either PlainTextBody or HtmlBody must be used or use a ApiTemplate with the AmpBody 
        /// </remarks> 
        public string HtmlBody { get; set; }

        /// <summary>
        /// Gets or sets the AMP HTML portion of the message body.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// Either PlainTextBody or HtmlBody must be used or use a ApiTemplate with the AmpBody  with the AmpBody
        /// See https://amp.dev/documentation/ for more information on AMP implementation
        /// </remarks> 
        public string AmpBody { get; set; }

        /// <summary>
        /// Gets or sets the Api Template for the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// Either PlainTextBody or HtmlBody must be used or use a ApiTemplate with the AmpBody 
        /// </remarks> 
        public int? ApiTemplate { get; set; }

        /// <summary>
        /// Gets or sets the custom MailingId for the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// See https://www.socketlabs.com/blog/best-practices-for-using-custom-mailingids-and-messageids/ for more information.
        /// </remarks> 
        public string MailingId { get; set; }

        /// <summary>
        /// Gets or sets the custom MessageId for the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        public string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the From address of the message.
        /// </summary>
        /// <remarks>
        /// (Required)
        /// </remarks> 
        public IEmailAddress From { get; set; } = new EmailAddress();

        /// <summary>
        /// Gets or sets the Reply To address for the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        public IEmailAddress ReplyTo { get; set; } = new EmailAddress();

        /// <summary>
        /// Gets or sets the list of To recipients for the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        public IList<IBulkRecipient> To { get; set; } = new List<IBulkRecipient>();

        /// <summary>
        /// A list of file attachments for the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        public IList<IAttachment> Attachments { get; set; } = new List<IAttachment>();

        /// <summary>
        /// A dictionary containing MergeData items that will be global across the whole message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        public IDictionary<string, string> GlobalMergeData { get; set; } = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

        /// <summary>
        /// The optional character set for your message.
        /// </summary>
        /// <remarks>
        /// (Optional) Default is UTF8
        /// </remarks> 
        public string CharSet { get; set; }

        /// <summary>
        /// A list of custom message headers added to the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        public IList<ICustomHeader> CustomHeaders { get; set; } = new List<ICustomHeader>();

        /// <summary>
        /// Returns the number of recipients and subject for the message, useful for debugging.
        /// </summary>
        public override string ToString()
        {
            return $"Recipients: {To?.Count ?? 0}, Subject: '{Subject}'";
        }
    }
}