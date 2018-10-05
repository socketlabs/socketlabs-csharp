using System;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic.Invalid
{
    public class BasicSendWithInvalidFrom: IExample
    {
        public SendResponse RunExample()
        {
            var message = new BasicMessage();

            message.Subject = "Sending A Test Message";
            message.HtmlBody = "<html>This is the Html Body of my message.</html>";
            message.PlainTextBody = "This is the Plain Text Body of my message.";

            message.From.Email = "!@#$!@#$!@#$@#!$";
            message.ReplyTo.Email = "replyto@example.com";

            message.To.Add("recipient@example.com");

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
