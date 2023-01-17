namespace SocketLabs.InjectionApi
{
    /// <summary>
    /// Enumerated result of the client send
    /// </summary>
    public enum SendResult
    {
        /// <summary>
        /// An error has occured that was unforeseen
        /// </summary>
        UnknownError,

        /// <summary>
        /// A timeout occurred sending the message
        /// </summary>
        Timeout,

        /// <summary>
        /// Successful send of message
        /// </summary>
        Success,

        /// <summary>
        /// Warnings were found while sending the message
        /// </summary>
        Warning,

        /// <summary>
        /// Internal server error
        /// </summary>
        InternalError,

        /// <summary>
        /// Message size has exceeded the size limit
        /// </summary>
        MessageTooLarge,

        /// <summary>
        /// Message exceeded maximum recipient count in the message
        /// </summary>
        TooManyRecipients,

        /// <summary>
        /// Invalid data was found on the message
        /// </summary>
        InvalidData,

        /// <summary>
        /// The account is over the send quota, rate limit exceeded
        /// </summary>
        OverQuota,

        /// <summary>
        /// Too many errors occurred sending the message
        /// </summary>
        TooManyErrors,

        /// <summary>
        /// The ServerId/ApiKey combination is invalid
        /// </summary>
        InvalidAuthentication,

        /// <summary>
        /// The account has been disabled
        /// </summary>
        AccountDisabled,

        /// <summary>
        /// Too many messages were found in the request
        /// </summary>
        TooManyMessages,

        /// <summary>
        /// No valid recipients were found in the message
        /// </summary>
        NoValidRecipients,

        /// <summary>
        /// An invalid recipient were found on the message
        /// </summary>
        InvalidAddress,

        /// <summary>
        /// An invalid attachment were found on the message
        /// </summary>
        InvalidAttachment,

        /// <summary>
        /// No message body was found in the message
        /// </summary>
        NoMessages,

        /// <summary>
        /// No message body was found in the message
        /// </summary>
        EmptyMessage,

        /// <summary>
        /// No subject was found in the message
        /// </summary>
        EmptySubject,

        /// <summary>
        /// An invalid from address was found on the message
        /// </summary>
        InvalidFrom,

        /// <summary>
        /// No To addresses were found in the message
        /// </summary>
        EmptyToAddress,

        /// <summary>
        /// No valid message body was found in the message
        /// </summary>
        NoValidBodyParts,

        /// <summary>
        /// An invalid TemplateId was found in the message
        /// </summary>
        InvalidTemplateId,

        /// <summary>
        /// The specified TemplateId has no content for the message
        /// </summary>
        TemplateHasNoContent,

        /// <summary>
        /// A conflict occurred on the message body of the message
        /// </summary>
        MessageBodyConflict,

        /// <summary>
        /// Invalid MergeData was found on the message
        /// </summary>
        InvalidMergeData,

        /// <summary>
        /// Authentication Error, Missing or invalid ServerId or ApiKey
        /// </summary>
        AuthenticationValidationFailed,

        /// <summary>
        /// From email address is missing in the message
        /// </summary>
        EmailAddressValidationMissingFrom,

        /// <summary>
        /// From email address in the message in invalid
        /// </summary>
        EmailAddressValidationInvalidFrom,

        /// <summary>
        /// Message exceeded maximum recipient count in the message
        /// </summary>
        RecipientValidationMaxExceeded,

        /// <summary>
        /// No recipients were found in the message
        /// </summary>
        RecipientValidationNoneInMessage,

        /// <summary>
        /// To addresses are missing in the message
        /// </summary>
        RecipientValidationMissingTo,

        /// <summary>
        /// Invalid ReplyTo address found
        /// </summary>
        RecipientValidationInvalidReplyTo,

        /// <summary>
        /// Invalid recipients found
        /// </summary>
        RecipientValidationInvalidRecipients,

        /// <summary>
        /// No subject was found in the message
        /// </summary>
        MessageValidationEmptySubject,

        /// <summary>
        /// No message body was found in the message
        /// </summary>
        MessageValidationEmptyMessage,

        /// <summary>
        /// Invalid custom headers found
        /// </summary>
        MessageValidationInvalidCustomHeaders,

        /// <summary>
        /// Invalid metadata found
        /// </summary>
        MessageValidationInvalidMetadata,
    }
}