using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketLabs.InjectionApi.Tests
{
    [TestClass()]
    public class SocketLabsClientTests
    {
        [TestMethod()]
        public async Task SendAsyncTest()
        {
            int serverId = 999;
            string bearerApiKey = "abcdefjhijklmnopqrst.uvwxyzabcdefghijklmnopqrstuvwxyz12345678";

            var client = new SocketLabsClient(serverId, bearerApiKey);
            client.EndpointUrl = "https://example.local";
            client.NumberOfRetries = 1;

            var message = new BasicMessage();
            message.To.Add("test@example.local");
            message.From = new EmailAddress("test@example.local");
            message.Subject = "Test";
            message.HtmlBody = "<p>This is a test!</p>";

            await Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await client.SendAsync(message, CancellationToken.None));

            // The client should throw the same exception on subsequent calls.
            await Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await client.SendAsync(message, CancellationToken.None));
        }
    }
}