

namespace SocketLabs.InjectionApi.Core
{
    public interface IApiKeyParser
    {
        ApiKeyParseResult Parse(string wholeApiKey);
    }
}
