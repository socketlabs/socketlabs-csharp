using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic
{
    public class BasicSendWithAttachment : IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            var message = new BasicMessage();

            message.Subject = "Sending An Email With An Attachment";
            message.HtmlBody = "<body><p><strong>Lorem Ipsum</strong></p></body>";
            message.PlainTextBody = "Lorem Ipsum";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";
            message.To.Add("recipient1@example.com");

            var attachment = message.Attachments.Add("bus.png", MimeType.PNG, @".\examples\img\bus.png");

            attachment.CustomHeaders.Add(new CustomHeader("Color", "Orange"));
            attachment.CustomHeaders.Add(new CustomHeader("Place", "Beach"));

            return client.Send(message);
        }
    }
}
