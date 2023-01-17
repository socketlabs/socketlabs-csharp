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

            // Build the message
            var message = new BasicMessage();

            message.Subject = "Sending Basic Complex Example";
            message.HtmlBody = "<body><p><strong>Lorem Ipsum</strong></p><br /><img src=\"cid:Bus\" /></body>";
            message.PlainTextBody = "Lorem Ipsum";
            message.AmpBody = "<!doctype html>" +
            "<html amp4email>" +
            "<head>" +
            "  <meta charset=\"utf-8\">" +
            "  <script async src=\"https://cdn.ampproject.org/v0.js\"></script>" +
            "  <style amp4email-boilerplate>body{visibility:hidden}</style>" +
            "  <style amp-custom>" +
            "    h1 {" +
            "      margin: 1rem;" +
            "    }" +
            "  </style>" +
            "</head>" +
            "<body>" +
            "  <h1>This is the AMP Html Body of my message</h1>" +
            "</body>" +
            "</html>";

            message.CharSet = "utf-8";

            // Add recipients
            message.To.Add("recipient1@example.com");
            message.To.Add("recipient2@example.com", "Recipient #2");

            // Set other properties as needed
            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";

            message.MailingId = "MyMailingId";
            message.MessageId = "MyMsgId";

            message.CustomHeaders.Add("x-mycustomheader", "I am a message header");

            var metadata = new List<IMetadata>()
            {
                new Metadata("example-type", "basic-send-complex"),
                new Metadata()
                {
                    Key = "message-contains",
                    Value = "attachments, headers"
                }
            };
            message.Metadata.Add(metadata);
            message.Metadata.Add("x-mycustommetadata", "I am custom metadata");
            message.Metadata.Add(new Metadata("testMessageHeader", "I am metadata"));

            message.Tags.Add("Basic-Complex-Example");
            message.Tags.Add("c#-Example");

            var attachment = message.Attachments.Add("bus.png", MimeType.PNG, @".\examples\img\bus.png");
            attachment.ContentId = "Bus";

            // Send the message
            return client.Send(message);
        }
    }
}
