using System.Collections.Generic;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Bulk
{
    public class BulkSendWithMergeData : IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            var message = new BulkMessage();

            // Add some global merge data
            message.GlobalMergeData.Add("Motto", "When hitting the Inbox matters!");

            message.Subject = "Sending A BulkSend with MergeData";
            message.HtmlBody = "<html>" +
                               "<head><title>Merge Data Example</title></head>" +
                               "<body>" +
                               "     <h1>Global Merge Data</h1>" +
                               "     <p>CompanyMotto = %%Motto%%</p>" +
                               "     <h1>Per Recipient Merge Data</h1>" +
                               "     <p>EyeColor = %%EyeColor%%</p>" +
                               "     <p>HairColor = %%HairColor%%</p>" +
                               "</body>" +
                               "</html>";
            message.PlainTextBody = "Global Merge Data" +
                                    "CompanyMotto = %%Motto%%" +
                                    "     " +
                                    "Per Recipient Merge Data" +
                                    "     EyeColor = %%EyeColor%%" +
                                    "     HairColor = %%HairColor%%";

            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";

            // Add recipients with MergeData
            var mergeDataFor1 = new Dictionary<string, string>();
            mergeDataFor1.Add("EyeColor", "Blue");
            mergeDataFor1.Add("HairColor", "Blond");

            message.To.Add("recipient1@example.com", mergeDataFor1);

            var recipient2 = message.To.Add("recipient2@example.com", "Recipient #1");
            recipient2.MergeData.Add("EyeColor", "Green");
            recipient2.MergeData.Add("HairColor", "Brown");

            var recipient3 = new BulkRecipient("recipient3@example.com");
            recipient3.AddMergeFields("EyeColor", "Hazel");
            recipient3.AddMergeFields("HairColor", "Black");
            message.To.Add(recipient3);

            return client.Send(message);

        }
    }
}
