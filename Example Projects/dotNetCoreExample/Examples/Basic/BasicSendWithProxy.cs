using System.Net;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic
{
    public class BasicSendWithProxy : IExample
    {
        public SendResponse RunExample()
        {
            var proxy = new WebProxy("http://localhost.:8888", false);

            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey, proxy)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            var message = new BasicMessage();

            message.Subject = "Sending A Test Message Through Proxy";
            message.HtmlBody = "<html>This is the Html Body of my message.</html>";
            message.PlainTextBody = "This is the Plain Text Body of my message.";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";
            message.To.Add("recipient1@example.com");

            return client.Send(message);
        }
    }
}
