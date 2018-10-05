using System.IO;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic
{
    class BasicSendFromHtmlFile : IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            var message = new BasicMessage();

            message.Subject = "Simple Html file with text";
            message.HtmlBody = File.ReadAllText(@".\examples\html\SimpleEmail.html");
            message.PlainTextBody = "This is the Plain Text Body of my message.";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";
            message.To.Add("recipient1@example.com");

            return client.Send(message);
            
        }
    }
}
