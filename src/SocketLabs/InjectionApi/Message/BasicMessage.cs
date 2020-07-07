using System.Collections.Generic;

namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// A basic email message similar to one created in a personal email client such as Outlook.
    /// This message can have many recipients of different types, such as To, CC, and BCC.  This
    /// message does not support merge fields.
    /// </summary>
    /// <example>
    /// <code>
    /// var message = new BasicMessage();
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
    /// var email1 = new EmailAddress("recipient1@example.com");
    /// message.To.Add(email1);
    /// 
    /// var email2 = new EmailAddress("recipient2@example.com", "Recipient #2");
    /// message.To.Add(email2); 
    ///</code>
    /// </example>
    /// <seealso cref="IMessageBase"/>
    /// <seealso cref="IBasicMessage"/>
    
    public class BasicMessage : IBasicMessage
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
        /// Gets or sets the AMP portion of the message body.
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
        /// Gets or sets the From address.
        /// </summary>
        /// <remarks>
        /// (Required)
        /// </remarks> 
        public IEmailAddress From { get; set; } = new EmailAddress();

        /// <summary>
        /// Gets or sets the Reply To address.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        public IEmailAddress ReplyTo { get; set; } = new EmailAddress();

        /// <summary>
        /// Gets or sets the list of To recipients.
        /// </summary> 
        /// <remarks>
        /// (Required)
        /// </remarks> 
        public IList<IEmailAddress> To { get; set; } = new List<IEmailAddress>();

        /// <summary>
        /// Gets or sets the list of CC recipients.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        public IList<IEmailAddress> Cc { get; set; } = new List<IEmailAddress>();

        /// <summary>
        /// Gets or sets the list of BCC recipients.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        public IList<IEmailAddress> Bcc { get; set; } = new List<IEmailAddress>();

        /// <summary>
        /// Gets or sets the list of attachments.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        public IList<IAttachment> Attachments { get; set; } = new List<IAttachment>();

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