using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Basic
{
    public class BasicSendWithAmpBody : IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            // Build the message
            var message = new BasicMessage();

            message.Subject = "Sending A Test Message";
            message.HtmlBody = "<html>This Html text will show if Amp is not supported/not implemented properly.</html>";
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




            // Add recipients
            message.To.Add("recipient1@example.com");
            message.To.Add("recipient2@example.com", "Recipient #2");
            message.To.Add(new EmailAddress("recipient3@example.com"));
            message.To.Add(new EmailAddress("recipient4@example.com", "Recipient #4"));

            // Set other properties as needed
            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";

            //Send message
            return client.Send(message);
        }
    }
}
