using SocketLabs.InjectionApi.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocketLabs.InjectionApi.Core.Models
{
    /// <summary>
    /// The result of an Api Key Parse Attempt
    /// </summary>
    public class ApiKeyParseResult
    {
        /// <summary>
        /// The object representing the parsed Api Key
        /// </summary>
        public ApiKey ApiKey { get; set; }

        /// <summary>
        /// A code describing the result of the attempt to parse the Api key.
        /// </summary>
        public ApiKeyParseResultCode ResultCode { get; set; }
    }
}
