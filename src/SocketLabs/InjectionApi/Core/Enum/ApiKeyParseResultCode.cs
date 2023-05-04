using System;
using System.Collections.Generic;
using System.Text;

namespace SocketLabs.InjectionApi.Core.Enum
{
    /// <summary>
    /// A code describing the result of an attempt to parse an Api key.
    /// </summary>
    public enum ApiKeyParseResultCode
    {

        /// <summary>
        /// No result could be produced.
        /// </summary>
        None,
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
        /// Key was successfully parsed.
        /// </summary>
        Success
    }
}
