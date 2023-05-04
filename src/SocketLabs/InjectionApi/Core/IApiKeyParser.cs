using SocketLabs.InjectionApi.Core.Enum;
using SocketLabs.InjectionApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocketLabs.InjectionApi.Core
{
    public interface IApiKeyParser
    {
        ApiKeyParseResult Parse(string wholeApiKey);
    }
}
