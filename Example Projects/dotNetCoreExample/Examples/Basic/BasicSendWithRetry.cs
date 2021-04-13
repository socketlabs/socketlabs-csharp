using System;
using System.Net;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic
{
    public class BasicSendWithRetry : IExample
    {
        public SendResponse RunExample()
        {
            var proxy = new WebProxy("http://localhost:4433", false);

            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey, proxy)
            {
                EndpointUrl = ExampleConfig.TargetApi,
                RequestTimeout = 120,
                NumberOfRetries = 3
            };

            var message = new BasicMessage();

            message.Subject = "Sending A Test Message With Retry Enabled";
            message.HtmlBody = "<html>This is the Html Body of my message.</html>";
            message.PlainTextBody = "This is the Plain Text Body of my message.";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";
            message.To.Add("recipient1@example.com");

            return client.Send(message);
        }
    }
}
