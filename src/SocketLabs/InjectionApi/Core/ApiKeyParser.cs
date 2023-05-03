using System;
using System.Collections.Generic;
using System.Text;
using SocketLabs.InjectionApi.Core.Enum;
using SocketLabs.InjectionApi.Core.Models;

namespace SocketLabs.InjectionApi.Core
{
    /// <summary>
    /// Parses a provided api key and provides a result
    /// </summary>
    public class ApiKeyParser : IApiKeyParser
    {
        /// <summary>
        /// Parses the provided Api key.
        /// </summary>
        /// <param name="wholeApiKey"></param>
        /// <returns>An ApiKeyParseResult with the api key data and result code from the parse.</returns>
        public ApiKeyParseResult Parse(string wholeApiKey)
        {
            var key = new ApiKey { IsValidFormat = false };
            if (string.IsNullOrWhiteSpace(wholeApiKey))
                return new ApiKeyParseResult { ApiKey = null, ResultCode = ApiKeyParseResultCode.InvalidEmptyOrWhitespace };

            //if (!wholeApiKey.StartsWith("SL.", StringComparison.InvariantCulture))
            //    return new ApiKeyParseResult {ApiKey = null, ResultCode = ApiKeyParseResultCode.InvalidMissingProperPrefix};

            //extract public part
            var maxCount = Math.Min(50, wholeApiKey.Length);
            var publicPartEnd = wholeApiKey.IndexOf('.', startIndex: 0, maxCount); //don't check more than 50 chars
            if (publicPartEnd == -1)
                return new ApiKeyParseResult { ApiKey = null, ResultCode = ApiKeyParseResultCode.InvalidUnableToExtractPublicPart };
            var publicPart = wholeApiKey.Substring(0, publicPartEnd);


            //now extract the private part
            if (wholeApiKey.Length <= publicPartEnd + 1)
                return new ApiKeyParseResult { ApiKey = null, ResultCode = ApiKeyParseResultCode.InvalidUnableToExtractSecretPart };
            var privatePart = wholeApiKey.Substring(publicPartEnd + 1);

            //success
            return new ApiKeyParseResult
            {
                ApiKey = new ApiKey
                {
                    PublicPart = publicPart,
                    PrivatePart = privatePart,
                    IsValidFormat = true,
                },
                ResultCode = ApiKeyParseResultCode.Success
            };
        }
    }
}
