using System.Collections.Generic;

namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// A basic email message similar to one created in a personal email client such as Outlook.
    /// This message can have many recipients of different types, such as To, CC, and BCC.  This
    /// message does not support merge fields.
    /// </summary>
    public interface IBasicMessage : IMessageBase
    {
        /// <summary>
        /// Gets or sets the list of To recipients.
        /// </summary>
        /// <remarks>
        /// (Required)
        /// </remarks> 
        IList<IEmailAddress> To { get; set; }

        /// <summary>
        /// Gets or sets the list of CC recipients.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        IList<IEmailAddress> Cc { get; set; }

        /// <summary>
        /// Gets or sets the list of BCC recipients.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        IList<IEmailAddress> Bcc { get; set; }
    }
}