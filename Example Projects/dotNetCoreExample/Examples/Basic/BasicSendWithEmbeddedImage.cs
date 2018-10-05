using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic
{
    public class BasicSendWithEmbeddedImage : IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            var message = new BasicMessage();

            message.Subject = "Sending An Email With An Embedded Image";
            message.HtmlBody = "<body><p><strong>Lorem Ipsum</strong></p><br /><img src=\"cid:bus\" /></body>";
            message.PlainTextBody = "Lorem Ipsum";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";
            message.To.Add("recipient1@example.com");

            var attachment = message.Attachments.AddAsync("bus.png", MimeType.PNG, @".\examples\img\bus.png").Result;
            attachment.ContentId = "bus";

            return client.Send(message);
        }
    }
}
