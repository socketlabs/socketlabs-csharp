using System;
using System.Collections.Generic;
using System.Text;

namespace SocketLabs.InjectionApi.Core
{
    /// <summary>
    /// A code describing the result of an attempt to parse an Api key.
    /// </summary>
    public enum ApiKeyParseResult
    {

        /// <summary>
        /// No result could be produced.
        /// </summary>
        None,
        /// <summary>
        /// The key length was found.
        /// </summary>
        InvalidKeyLength,

        /// <summary>
        /// The key format was found.
        /// </summary>
        InvalidKeyFormat,
        /// <summary>
        /// The key was found to be blank or invalid.
        /// </summary>
        InvalidEmptyOrWhitespace,
        /// <summary>
        /// The public portion of the key was unable to be parsed.
        /// </summary>
        InvalidUnableToExtractPublicPart,
        /// <summary>
        /// The secret portion of the key was unable to be parsed.
        /// </summary>
        InvalidUnableToExtractSecretPart,
        /// <summary>
        /// The public portion of the key is the incorrect length.
        /// </summary>
        InvalidPublicPartLength,
        /// <summary>
        /// The secret portion of the key is the incorrect length.
        /// </summary>
        InvalidSecretPartLength,
        /// <summary>
        /// Key was successfully parsed.
        /// </summary>
        Success
    }
}
