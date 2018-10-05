using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic
{
    public class BasicSendWithApiTemplate : IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            var message = new BasicMessage();

            message.Subject = "Sending Using a Template";
            message.ApiTemplate = 1;

            message.MessageId = "DesignerGen";
            message.MailingId = "BasicSend";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";

            message.To.Add("recipient1@example.com");

            return client.Send(message);

        }
    }
}
