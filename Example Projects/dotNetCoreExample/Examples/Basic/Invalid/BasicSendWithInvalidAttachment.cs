using System;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic.Invalid
{
    public class BasicSendWithInvalidAttachment : IExample
    {
        public SendResponse RunExample()
        {
            var message = new BasicMessage();

            message.Subject = "Sending A Test Message";
            message.HtmlBody = "<html>This is the Html Body of my message.</html>";
            message.PlainTextBody = "This is the Plain Text Body of my message.";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";

            message.To.Add("recipient1@example.com");
            message.To.Add("recipient2@example.com");

            message.Attachments.Add("bus", MimeType.PNG, new byte[]{});

            using (var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            })
            {
                try
                {
                    var response = client.Send(message);
                    return response;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
        }
    }
}
