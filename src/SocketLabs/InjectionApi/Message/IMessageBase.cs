using System.Collections.Generic;

namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// The IMessageBase is an interface that contains fields used by the Injection API and is implemented by all message types.
    /// </summary>
    public interface IMessageBase
    {
        /// <summary>
        /// Gets or sets the instance of the message Subject.
        /// </summary>
        /// <remarks>
        /// (Required )
        /// </remarks> 
        string Subject { get; set; }

        /// <summary>
        /// Gets or sets the plain text portion of the message body.        
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// Either PlainTextBody or HtmlBody must be used with the AmpBody or use a ApiTemplate
        /// </remarks> 
        string PlainTextBody { get; set; }

        /// <summary>
        /// Gets or sets the HTML portion of the message body.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// Either PlainTextBody or HtmlBody must be used with the AmpBody or use a ApiTemplate
        /// </remarks> 
        string HtmlBody { get; set; }

        /// <summary>
        /// Gets or sets the AMP HTML portion of the message body.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// Either PlainTextBody or HtmlBody must be used with the AmpBody or use a ApiTemplate
        /// See https://amp.dev/documentation/ for more information on AMP implementation
        /// </remarks> 
        string AmpBody { get; set; }

        /// <summary>
        /// Gets or sets the Api Template for the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// Either PlainTextBody or HtmlBody must be used or use a ApiTemplate with the AmpBody
        /// </remarks> 
        int? ApiTemplate { get; set; }

        /// <summary>
        /// Gets or sets the custom MailingId for the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// See https://www.socketlabs.com/blog/best-practices-for-using-custom-mailingids-and-messageids/ for more information.
        /// </remarks> 
        string MailingId { get; set; }

        /// <summary>
        /// Gets or sets the custom MessageId for the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the From address.
        /// </summary>
        /// <remarks>
        /// (Required)
        /// </remarks> 
        IEmailAddress From { get; set; }

        /// <summary>
        /// An optional ReplyTo address for the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        IEmailAddress ReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the list of attachments.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        IList<IAttachment> Attachments { get; }

        /// <summary>
        /// The optional character set for your message.
        /// </summary>
        /// <remarks>
        /// (Optional) Default is UTF8
        /// </remarks> 
        string CharSet { get; set; }

        /// <summary>
        /// A list of custom message headers added to the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        IList<ICustomHeader> CustomHeaders { get; set; }


        /// <summary>
        /// A list of metadata headers added to the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        IList<IMetadata> Metadata { get; set; }

        /// <summary>
        /// A list of tag headers added to the message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        IList<string> Tags { get; set; }
    }
}