using System.Collections.Generic;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Bulk
{
    public class BulkSendWithAsciiCharsetMergeData : IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            var message = new BulkMessage();

            message.CharSet = "ASCII";
            
            message.Subject = "Sending A BulkSend with MergeData";
            message.HtmlBody = "<html>" +
                               "<head><title>ASCII Merge Data Example</title></head>" +
                               "<body>" +
                               "     <h1>Merge Data</h1>" +
                               "     <p>Complete? = %%Complete%%</p>" +
                               "</body>" +
                               "</html>";
            message.PlainTextBody = "Merge Data" +
                                    "     Complete? = %%Complete%%";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";

            var mergeDataFor1 = new Dictionary<string, string>
            {
                { "Complete", "✔" }
            };
            message.To.Add("recipient1@example.com", mergeDataFor1);

            var recipient2 = message.To.Add("recipient2@example.com", "Recipient #1");
            recipient2.MergeData.Add("Complete", "✔");

            var recipient3 = new BulkRecipient("recipient3@example.com");
            recipient3.MergeData.Add("Complete", "✘");
            message.To.Add(recipient3);

            return client.Send(message);
        }
    }
}
