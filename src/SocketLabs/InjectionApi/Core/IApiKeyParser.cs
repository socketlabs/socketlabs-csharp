

namespace SocketLabs.InjectionApi.Core
{
    internal interface IApiKeyParser
    {
        ApiKeyParseResult Parse(string wholeApiKey);
    }
}
