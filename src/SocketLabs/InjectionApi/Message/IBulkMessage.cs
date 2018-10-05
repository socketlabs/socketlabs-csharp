using System.Collections.Generic;

namespace SocketLabs.InjectionApi.Message
{
    /// <summary>
    /// A bulk message usually contains a single recipient per message 
    /// and is generally used to send the same content to many recipients, 
    /// optionally customizing the message via the use of MergeData.
    /// </summary>
    public interface IBulkMessage : IMessageBase
    {
        /// <summary>
        /// Gets or sets the list of To recipients.
        /// </summary>
        /// <remarks>
        /// (Required)
        /// </remarks> 
        IList<IBulkRecipient> To { get; set; }

        /// <summary>
        /// A dictionary containing MergeData items that will be global across the whole message.
        /// </summary>
        /// <remarks>
        /// (Optional)
        /// </remarks> 
        IDictionary<string, string> GlobalMergeData { get; set; }
    }
}