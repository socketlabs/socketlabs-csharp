using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic
{
    public class BasicSend: IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            var message = new BasicMessage();
            
            message.Subject = "Sending A Test Message";
            message.HtmlBody = "<html>This is the Html Body of my message.</html>";
            message.PlainTextBody = "This is the Plain Text Body of my message.";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";

            message.To.Add("recipient1@example.com");
            message.To.Add("recipient2@example.com", "Recipient #1");
            message.To.Add(new EmailAddress("recipient3@example.com"));
            message.To.Add(new EmailAddress("recipient4@example.com", "Recipient #4"));

            return client.Send(message);
        }
    }
}
