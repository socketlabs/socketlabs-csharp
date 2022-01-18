using System;
using System.Collections.Generic;
using System.Linq;
using SocketLabs.InjectionApi.Core.Serialization;
using SocketLabs.InjectionApi.Message;

namespace SocketLabs.InjectionApi.Core
{
    /// <summary>
    /// Used by the Send function of the SocketLabsClient to generate an InjectionRequest for the Injection Api
    /// </summary>
    internal class InjectionRequestFactory : IInjectionRequestFactory
    {
        private readonly int _serverId;
        private readonly string _apiKey;

        /// <summary>
        /// Creates a new instance of the <c>InjectionRequestFactory</c>.
        /// </summary>
        /// <param name="serverId">Your SocketLabs ServerId number.</param>
        /// <param name="apiKey">Your SocketLabs Injection API key.</param>
        public InjectionRequestFactory(int serverId, string apiKey)
        {
            _serverId = serverId;
            _apiKey = apiKey;
        }

        /// <summary>
        /// Generate the <c>InjectionRequest</c> for sending to the Injection Api
        /// </summary>
        /// <param name="message">An <c>IBasicMessage</c> object to be sent.</param> 
        /// <returns>An <c>InjectionRequest</c> for sending to the Injection Api</returns>
        public InjectionRequest GenerateRequest(IBasicMessage message)
        {
            var request = new InjectionRequest(_serverId, _apiKey);

            var jsonMsg = GenerateBaseMessageJson(message);
            jsonMsg.To = PopulateList(message.To);
            jsonMsg.Cc = PopulateList(message.Cc);
            jsonMsg.Bcc = PopulateList(message.Bcc);

            request.Messages.Add(jsonMsg);

            if (message.ReplyTo != null)
                jsonMsg.ReplyTo = new AddressJson(message.ReplyTo.Email, message.ReplyTo.FriendlyName);

            return request;
        }

        /// <summary>
        /// Generate the <c>InjectionRequest</c> for sending to the Injection Api
        /// </summary>
        /// <param name="message">An <c>IBulkMessage</c> object to be sent.</param>
        /// <returns>An <c>InjectionRequest</c> for sending to the Injection Api</returns>
        public InjectionRequest GenerateRequest(IBulkMessage message)
        {
            var request = new InjectionRequest(_serverId, _apiKey);

            var jsonMsg = GenerateBaseMessageJson(message);

            // Replace the "To" recipients with links to merge field data
            jsonMsg.To = new List<AddressJson>{new AddressJson("%%DeliveryAddress%%", "%%RecipientName%%")};

            // handle merge data per recipient for message 
            var mergeDataForEmail = GetBulkMergeFields(message.To);
            jsonMsg.MergeData.PerMessage = mergeDataForEmail;

            // handle global (per message) merge data
            jsonMsg.MergeData.Global = PopulateMergeData(message.GlobalMergeData);

            request.Messages.Add(jsonMsg);

            return request;
        }


        /// <summary>
        /// Generate the base <c>MessageJson</c> for a message
        /// </summary>
        /// <param name="message">The base interface, <c>IMessageBase</c>, of the message to be sent.</param>
        /// <returns>A <c>MessageJson</c> object for generating an InjectionRequest</returns>
        internal virtual MessageJson GenerateBaseMessageJson(IMessageBase message)
        {
            var jsonMsg = new MessageJson
            {
                Subject = message.Subject,
                TextBody = message.PlainTextBody,
                HtmlBody = message.HtmlBody,
                AmpBody = message.AmpBody,
                MailingId = message.MailingId,
                MessageId = message.MessageId,
                CharSet = message.CharSet,
                CustomHeaders = PopulateCustomHeaders(message.CustomHeaders),
                From = new AddressJson(message.From.Email, message.From.FriendlyName),
                Attachments = PopulateList(message.Attachments)
            };

            if (message.ReplyTo != null)
                jsonMsg.ReplyTo = new AddressJson(message.ReplyTo.Email, message.ReplyTo.FriendlyName);

            if (message.ApiTemplate.HasValue)
                jsonMsg.ApiTemplate = message.ApiTemplate.ToString();

            return jsonMsg;
        }

        /// <summary>
        /// Converting a <c><![CDATA[ IEnumerable<IAttachment> ]]></c> to a <c><![CDATA[ List<AttachmentJson> ]]></c>
        /// </summary>
        /// <param name="attachments">A <c><![CDATA[ IEnumerable<IAttachment> ]]></c> from the message</param>
        /// <returns>A <c><![CDATA[ List<AttachmentJson> ]]></c> used in generating an InjectionRequest</returns>
        internal virtual List<AttachmentJson> PopulateList(IEnumerable<IAttachment> attachments)
        {
            if (attachments == null)
                return null;

            var results = new List<AttachmentJson>();

            foreach (var attachment in attachments)
            {
                var attachmentJson = new AttachmentJson();
                attachmentJson.Name = attachment.Name;
                attachmentJson.ContentType = attachment.MimeType;
                attachmentJson.ContentId = attachment.ContentId;
                attachmentJson.Content = Convert.ToBase64String(attachment.Content);
                attachmentJson.CustomHeaders = PopulateCustomHeaders(attachment.CustomHeaders);

                results.Add(attachmentJson);
            }

            return results;
        }

        /// <summary>
        /// Converting a <c><![CDATA[ IList<ICustomHeader> ]]></c> to a <c><![CDATA[ List<CustomHeadersJson> ]]></c>
        /// </summary>
        /// <param name="customHeaders">A <c><![CDATA[ IList<ICustomHeader> ]]></c> from the message</param>
        /// <returns>A <c><![CDATA[ List<CustomHeadersJson> ]]></c> used in generating an InjectionRequest</returns>
        internal virtual List<CustomHeadersJson> PopulateCustomHeaders(IList<ICustomHeader> customHeaders)
        {
            var result = customHeaders?.Select(item => new CustomHeadersJson(item.Name, item.Value));
            return result?.ToList();
        }

        /// <summary>
        /// Converting a <c><![CDATA[ IEnumerable<IEmailAddress> ]]></c> to a <c><![CDATA[ List<AddressJson> ]]></c>
        /// </summary>
        /// <param name="recipients">A List of <c>IEmailAddress</c> from the message</param>
        /// <returns>A <c><![CDATA[ List<AddressJson> ]]></c> used in generating an InjectionRequest</returns>
        internal virtual List<AddressJson> PopulateList(IEnumerable<IEmailAddress> recipients)
        {
            var result = recipients?.Select(item => new AddressJson(item.Email, item.FriendlyName));
            return result?.ToList();
        }

        /// <summary>
        /// Converting a <c><![CDATA[ IEnumerable<IBulkRecipient> ]]></c> to a <c><![CDATA[ List<List<MergeFieldJson>> ]]></c>
        /// </summary>
        /// <param name="recipients">A <c><![CDATA[ IEnumerable<IBulkRecipient> ]]></c> from the message</param>
        /// <returns>A <c><![CDATA[ List<List<MergeFieldJson>> ]]></c> used in generating an InjectionRequest</returns>
        internal virtual List<List<MergeFieldJson>> GetBulkMergeFields(IEnumerable<IBulkRecipient> recipients)
        {
            var result = new List<List<MergeFieldJson>>();

            //each recipient get's their own list of merge fields
            foreach (var recipient in recipients)
            {
                // Get any merge data associated with the Recipients and put it in the MergeData section
                var recipientMergeFields = recipient.MergeData?.Select(mergeField => new MergeFieldJson(mergeField.Key, mergeField.Value)).ToList() ??
                                                 new List<MergeFieldJson>();

                recipientMergeFields.Add(new MergeFieldJson("DeliveryAddress", recipient.Email));

                //don't include friendly name if it hasn't been provided
                if (!string.IsNullOrWhiteSpace(recipient.FriendlyName))
                    recipientMergeFields.Add(new MergeFieldJson("RecipientName", recipient.FriendlyName));

                result.Add(recipientMergeFields);
            }

            return result;
        }

        /// <summary>
        /// Converting a <c><![CDATA[ IDictionary<string, string> ]]></c> of MergeData to a <c><![CDATA[ List<MergeFieldJson> ]]></c>
        /// </summary>
        /// <param name="mergeData">A <c><![CDATA[ IDictionary<string, string> ]]></c> of MergeData from the message</param>
        /// <returns>A <c><![CDATA[ List<MergeFieldJson> ]]></c> used in generating an InjectionRequest</returns>
        internal virtual List<MergeFieldJson> PopulateMergeData(IDictionary<string, string> mergeData)
        {

            var result = mergeData?.Select(item => new MergeFieldJson(item.Key, item.Value));
            return result?.ToList();
        }
    }
}
