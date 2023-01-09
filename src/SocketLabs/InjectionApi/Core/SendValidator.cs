using System;
using System.Collections.Generic;
using System.Linq;
using SocketLabs.InjectionApi.Message;

namespace SocketLabs.InjectionApi.Core
{
    /// <summary>
    /// Used by the <c>SocketLabsClient</c> to conduct basic validation on the message before sending to the Injection API.
    /// </summary>
    internal class SendValidator : ISendValidator
    {
        /// <summary>
        /// Maximum recipient threshold 
        /// </summary>
        internal readonly int MaximumRecipientsPerMessage = 50;

        /// <summary>
        /// Validate a basic email message before sending to the Injection API.
        /// </summary>
        /// <param name="message">An <c>IBasicMessage</c> object to be sent.</param> 
        /// <returns>A <c>SendResponse</c> with the validation results</returns>
        public SendResponse ValidateMessage(IBasicMessage message)
        {

            var result = ValidateIMessageBase(message);
            if (result == SendResult.Success)
                return ValidateRecipients(message);

            return new SendResponse() { Result = result };
        }

        /// <summary>
        /// Validate a bulk email message before sending to the Injection API.
        /// </summary>
        /// <param name="message">An <c>IBulkMessage</c> object to be sent.</param>
        /// <returns>A <c>SendResponse</c> with the validation results</returns>
        public SendResponse ValidateMessage(IBulkMessage message)
        {
            var result = ValidateIMessageBase(message);
            if (result == SendResult.Success)
                return ValidateRecipients(message);

            return new SendResponse() { Result = result };
        }

        /// <summary>
        /// Validate the ServerId and Api Key pair prior before sending to the Injection API.
        /// </summary>
        /// <param name="serverId">Your SocketLabs ServerId number.</param>
        /// <param name="apiKey">Your SocketLabs Injection API key.</param>
        /// <returns>A <c>SendResponse</c> with the validation results</returns>
        public SendResponse ValidateCredentials(int serverId, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) return new SendResponse() { Result = SendResult.AuthenticationValidationFailed }; 

            if (serverId == int.MinValue && serverId <= 0)  return new SendResponse() { Result = SendResult.AuthenticationValidationFailed }; 

            return new SendResponse() {Result = SendResult.Success};
        }

        /// <summary>
        /// Validate the required fields of a message. Fields validated are Subject, From Address, Reply To (if set), Message Body, and Custom Headers (if set)
        /// </summary>
        /// <param name="message">The base interface, <c>IMessageBase</c>, of the message to be sent.</param>
        /// <returns>The validation result as <c>SendResult</c> </returns>
        internal virtual SendResult ValidateIMessageBase(IMessageBase message)
        {
            if (!HasSubject(message)) return SendResult.MessageValidationEmptySubject;

            if (!HasFromAddress(message)) return SendResult.EmailAddressValidationMissingFrom;
            if (!message.From.IsValid) return SendResult.EmailAddressValidationInvalidFrom;

            if (!HasValidReplyTo(message)) return SendResult.RecipientValidationInvalidReplyTo;

            if (!HasMessageBody(message)) return SendResult.MessageValidationEmptyMessage;

            if (message.CustomHeaders != null && message.CustomHeaders.Any())
                if (!HasValidCustomHeaders(message.CustomHeaders)) return SendResult.MessageValidationInvalidCustomHeaders;

            if (message.Metadata != null && message.Metadata.Any())
                if (!HasValidMetadata(message.Metadata)) return SendResult.MessageValidationInvalidMetadata;

            return SendResult.Success;
        }

        /// <summary>
        /// Check if the message has a subject 
        /// </summary>
        /// <param name="message">The base interface, <c>IMessageBase</c>, of the message to be sent.</param>
        /// <returns><c>bool</c> result</returns>
        internal virtual bool HasSubject(IMessageBase message)
        {
            return !string.IsNullOrWhiteSpace(message.Subject);
        }

        /// <summary>
        /// Check if the message has a valid From Email Address
        /// </summary>
        /// <param name="message">The base interface, <c>IMessageBase</c>, of the message to be sent.</param>
        /// <returns><c>bool</c> result</returns>
        internal virtual bool HasFromAddress(IMessageBase message)
        {
            if (message.From == null)
                return false;

            if(string.IsNullOrEmpty(message.From.Email))
                return false;

            return true;
        }

        /// <summary>
        /// Check if the message has a Message Body
        /// </summary>
        /// <remarks>
        /// If an Api Template is specified it will override the HtmlBody, the AmpBody and/or the PlainTextBody.
        /// If no Api Template is specified the HtmlBody and/or the PlainTextBody must be set
        /// </remarks>
        /// <param name="message">The base interface, <c>IMessageBase</c>, of the message to be sent.</param>
        /// <returns><c>bool</c> result</returns>
        internal virtual bool HasMessageBody(IMessageBase message)
        {
            var hasApiTemplate = HasApiTemplate(message);
            if (hasApiTemplate) return true;

            var hasHtmlBody = !string.IsNullOrWhiteSpace(message.HtmlBody);
            var hasPlainTextBody = !string.IsNullOrWhiteSpace(message.PlainTextBody);

            return (hasHtmlBody || hasPlainTextBody);
        }

        /// <summary>
        /// Check if an ApiTemplate was specified and is valid
        /// </summary>
        /// <param name="message">The base interface, <c>IMessageBase</c>, of the message to be sent.</param>
        /// <returns><c>bool</c> result</returns>
        internal virtual bool HasApiTemplate(IMessageBase message)
        {
            return (message.ApiTemplate.HasValue && message.ApiTemplate != int.MinValue && message.ApiTemplate != 0);
        }

        /// <summary>
        /// If set, check if a ReplyTo email address is valid
        /// </summary>
        /// <param name="message">The base interface, <c>IMessageBase</c>, of the message to be sent.</param>
        /// <returns><c>bool</c> result</returns>
        internal virtual bool HasValidReplyTo(IMessageBase message)
        {
            if (String.IsNullOrWhiteSpace(message.ReplyTo.Email) && String.IsNullOrWhiteSpace(message.ReplyTo.FriendlyName)) return true;
            return message.ReplyTo.IsValid;
        }

        /// <summary>
        /// Validate email recipients for a basic message
        /// </summary>
        /// <remarks>
        /// Checks the To, Cc, and the Bcc recipient fields (List of <c>IEmailAddress</c>) for the following:
        /// <list type="bullet">
        /// <item>
        /// <description>At least 1 recipient is in the list.</description>
        /// </item>
        /// <item>
        /// <description>Cumulative count of recipients in all 3 lists do not exceed the <c>MaximumRecipientsPerMessage</c>.</description>
        /// </item>
        /// <item>
        /// <description>Recipients in lists are valid.</description>
        /// </item>
        /// </list>
        ///  If errors are found, the <c>SendResponse</c> will contain the invalid email addresses
        /// </remarks>
        /// <param name="message">An <c>IBasicMessage</c> object to be sent.</param> 
        /// <returns>A <c>SendResponse</c> with the validation results</returns>
        internal virtual SendResponse ValidateRecipients(IBasicMessage message)
        {
            var fullRecipientCount = GetFullRecipientCount(message);
            if (fullRecipientCount <= 0) return new SendResponse() { Result = SendResult.RecipientValidationNoneInMessage };

            if (fullRecipientCount > MaximumRecipientsPerMessage) return new SendResponse() { Result = SendResult.RecipientValidationMaxExceeded };

            var invalidRec = HasInvalidRecipients(message);
            if (invalidRec != null && invalidRec.Any())
                return new SendResponse()
                {
                    Result = SendResult.RecipientValidationInvalidRecipients,
                    AddressResults = invalidRec.ToArray()
                };

            return new SendResponse() { Result = SendResult.Success };
        }

        /// <summary>
        /// Validate email recipients for a bulk message
        /// </summary>
        /// <remarks>
        /// Checks the To recipient field (List of <c>IBulkRecipient</c>) for the following:
        /// <list type="bullet">
        /// <item>
        /// <description>At least 1 recipient is in the list.</description>
        /// </item>
        /// <item>
        /// <description>Recipients in list do not exceed the <c>MaximumRecipientsPerMessage</c>.</description>
        /// </item>
        /// <item>
        /// <description>Recipients in list are valid.</description>
        /// </item>
        /// </list>
        ///  If errors are found, the <c>SendResponse</c> will contain the invalid email addresses
        /// </remarks>
        /// <param name="message">An <c>IBulkMessage</c> object to be sent.</param> 
        /// <returns>A <c>SendResponse</c> with the validation results</returns>
        internal virtual SendResponse ValidateRecipients(IBulkMessage message)
        {
            if (message.To == null || message.To.Count <= 0) return new SendResponse() { Result = SendResult.RecipientValidationMissingTo };

            if (message.To.Count > MaximumRecipientsPerMessage) return new SendResponse() { Result = SendResult.RecipientValidationMaxExceeded };

            var invalidRec = HasInvalidRecipients(message);
            if (invalidRec != null && invalidRec.Any())
                return new SendResponse()
                {
                    Result = SendResult.RecipientValidationInvalidRecipients,
                    AddressResults = invalidRec.ToArray()
                };

            return new SendResponse() { Result = SendResult.Success };
        }

        /// <summary>
        /// Cumulative count of recipients in all 3 recipient lists To, Cc, and Bcc (List of <c>IEmailAddress</c>)
        /// </summary>
        /// <param name="message">An <c>IBasicMessage</c> object to be sent.</param> 
        /// <returns></returns>
        internal virtual int GetFullRecipientCount(IBasicMessage message)
        {
            var recipientCount = 0;
            if (message.To != null)
                recipientCount += message.To.Count;
            if (message.Cc != null)
                recipientCount += message.Cc.Count;
            if (message.Bcc != null)
                recipientCount += message.Bcc.Count;
            return recipientCount;
        }

        /// <summary>
        /// Check all 3 recipient lists To, Cc, and Bcc (List of <c>IEmailAddress</c>) for valid email addresses
        /// </summary>
        /// <param name="message">An <c>IBasicMessage</c> object to be sent.</param> 
        /// <returns>A List of <c>AddressResult</c> if an invalid email address is found.</returns>
        internal virtual List<AddressResult> HasInvalidRecipients(IBasicMessage message)
        {
            var result = new List<AddressResult>();

            var invalidTo = FindInvalidRecipients(message.To);
            if (invalidTo != null && invalidTo.Any())
                result.AddRange(invalidTo.ToList());

            var invalidCc = FindInvalidRecipients(message.Cc);
            if (invalidCc != null && invalidCc.Any())
                result.AddRange(invalidCc.ToList());

            var invalidBcc = FindInvalidRecipients(message.Bcc);
            if (invalidBcc != null && invalidBcc.Any())
                result.AddRange(invalidBcc.ToList());
            
            return result;
        }

        /// <summary>
        /// Check the To recipient list (List of <c>IEmailAddress</c>) for valid email addresses
        /// </summary>
        /// <param name="message">An <c>IBulkMessage</c> object to be sent.</param> 
        /// <returns>A List of <c>AddressResult</c> if an invalid email address is found.</returns>
        internal virtual List<AddressResult> HasInvalidRecipients(IBulkMessage message)
        {
            var result = new List<AddressResult>();

            var invalidTo = FindInvalidRecipients(message.To);
            if (invalidTo != null && invalidTo.Any())
                result.AddRange(invalidTo.ToList());

            return result;
        }

        /// <summary>
        /// Check <c><![CDATA[ IList<IEmailAddress> ]]></c> for valid email addresses
        /// </summary>
        /// <param name="recipients"><c><![CDATA[ IList<IEmailAddress> ]]></c> to validate</param>
        /// <returns>A <c><![CDATA[ List<AddressResult> ]]></c> if an invalid email address is found.</returns>
        /// <see cref="IEmailAddress"/>
        /// <see cref="AddressResult"/>
        internal virtual List<AddressResult> FindInvalidRecipients(IList<IEmailAddress> recipients)
        {
            var invalid = recipients?.Where(item => !item.IsValid).Select(x => new AddressResult()
            {
                Accepted = false,
                EmailAddress = x.Email,
                ErrorCode = "InvalidAddress"
            }).ToList();

            if (invalid != null && invalid.Any())
                return invalid;

            return null;
        }

        /// <summary>
        /// Check <c><![CDATA[ IList<IBulkRecipient> ]]></c> for valid email addresses
        /// </summary>
        /// <param name="recipients"><c><![CDATA[ IList<IBulkRecipient> ]]></c> to validate</param>
        /// <returns>A <c><![CDATA[ List<AddressResult> ]]></c> if an invalid email address is found.</returns>
        /// <see cref="IBulkRecipient"/>
        /// <see cref="AddressResult"/>
        internal virtual List<AddressResult> FindInvalidRecipients(IList<IBulkRecipient> recipients)
        {
            var invalid = recipients?.Where(item => !item.IsValid).Select(x => new AddressResult()
            {
                Accepted = false,
                EmailAddress = x.Email,
                ErrorCode = "InvalidAddress"
            }).ToList();

            if (invalid != null && invalid.Any())
                return invalid;

            return null;
        }

        /// <summary>
        /// Check if <c>ICustomHeader</c> in List are valid
        /// </summary>
        /// <param name="customHeaders"><c><![CDATA[ IList<ICustomHeader> ]]></c> to validate</param>
        /// <returns><c>bool</c> result</returns>
        internal virtual bool HasValidCustomHeaders(IList<ICustomHeader> customHeaders)
        {
            var result = customHeaders?.Where(item => !item.IsValid);
            return result == null || !result.Any();
        }

        /// <summary>
        /// Check if <c>IMetadata</c> in List are valid
        /// </summary>
        /// <param name="metadata"><c><![CDATA[ IList<IMetadata> ]]></c> to validate</param>
        /// <returns><c>bool</c> result</returns>
        internal virtual bool HasValidMetadata(IList<IMetadata> metadata)
        {
            var result = metadata?.Where(item => !item.IsValid);
            return result == null || !result.Any();
        }
    }
}
