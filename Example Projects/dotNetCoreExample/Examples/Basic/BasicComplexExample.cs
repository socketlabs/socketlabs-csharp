using System.Collections.Generic;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic
{
    public class BasicComplexExample : IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey);

            var message = new BasicMessage();

            message.To.Add("recipient1@example.com");
            message.To.Add("recipient2@example.com", "Recipient #2");

            message.Subject = "Sending Basic Complex Example";
            message.HtmlBody = "<body><p><strong>Lorem Ipsum</strong></p><br /><img src=\"cid:Bus\" /></body>";
            message.PlainTextBody = "Lorem Ipsum";
            message.CharSet = "utf-8";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";

            message.MailingId = "MyMailingId";
            message.MessageId = "MyMsgId";

            message.CustomHeaders.Add("x-mycustomheader", "I am a message header");

            var attachment = message.Attachments.Add("bus.png", MimeType.PNG, @".\examples\img\bus.png");
            attachment.ContentId = "Bus";

            return client.Send(message);
        }
    }
}
