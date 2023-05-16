using System;

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
            if (string.IsNullOrWhiteSpace(wholeApiKey))
                return ApiKeyParseResult.InvalidEmptyOrWhitespace;
            
            //extract public part
            var maxCount = Math.Min(50, wholeApiKey.Length);
            var publicPartEnd = wholeApiKey.IndexOf('.', startIndex: 0, maxCount); //don't check more than 50 chars
            if (publicPartEnd == -1)
                return ApiKeyParseResult.InvalidUnableToExtractPublicPart;
            
            //now extract the private part
            if (wholeApiKey.Length <= publicPartEnd + 1)
                return ApiKeyParseResult.InvalidUnableToExtractSecretPart;

            //success
            return ApiKeyParseResult.Success;
        }
    }
}
