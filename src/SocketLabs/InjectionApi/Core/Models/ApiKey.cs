using System;
using System.Collections.Generic;
using System.Text;

namespace SocketLabs.InjectionApi.Core.Models
{
    /// <summary>
    /// A class representing an Api Key
    /// </summary>
    public class ApiKey
    {
        /// <summary>
        /// The public part of the Api Key.
        /// </summary>
        public string PublicPart { get; set; } = "";

        /// <summary>
        /// The private part of the Api Key
        /// </summary>
        public string PrivatePart { get; set; } = "";

        /// <summary>
        /// A boolean value describing the validity of the Api Key format
        /// </summary>
        public bool IsValidFormat { get; set; }
    }
}
