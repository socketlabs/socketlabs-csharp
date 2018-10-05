using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic
{
    public class BasicSendWithAsciiCharset : IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            var message = new BasicMessage();

            message.Subject = "Sending An ASCII Charset Email";
            message.HtmlBody = "<body><p><strong>Lorem Ipsum</strong></p><br />Unicode: ✔ - Check</body>";
            message.PlainTextBody = "Lorem Ipsum";

            message.CharSet = "ASCII";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";
            message.To.Add("recipient1@example.com");

            return client.Send(message);
        }
    }
}
