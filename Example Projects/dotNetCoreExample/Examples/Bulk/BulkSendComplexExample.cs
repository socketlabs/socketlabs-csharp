using System.Text;
using System.Text.RegularExpressions;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Bulk
{
    public class BulkSendComplexExample : IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            // Build the message
            var message = new BulkMessage();

            // Add some global merge-data (These will be applied to all Recipients unless specifically overridden by Recipient level merge-data)
            message.GlobalMergeData.Add("Motto", "When hitting the Inbox matters!");
            message.GlobalMergeData.Add("Birthday", "Unknown");
            message.GlobalMergeData.Add("Age", "an unknown number of");
            message.GlobalMergeData.Add("UpSell", "BTW:  You are eligible for discount pricing when you upgrade your service!");

            // Add recipients with merge data
            var recipient1 = message.To.Add("recipient1@example.com");
            recipient1.MergeData.Add("Birthday", "08/05/1991");
            recipient1.MergeData.Add("Age", "27");
            
            var recipient2 = message.To.Add("recipient2@example.com");
            recipient2.MergeData.Add("Birthday", "04/12/1984");
            recipient2.MergeData.Add("Age", "34");
            recipient2.MergeData.Add("UpSell", "");  // This will override the Global Merge-Data for this specific Recipient

            var recipient3 = message.To.Add("recipient3@example.com"); // The merge-data for this Recipient will be populated with Global Merge-Data
            recipient3.FriendlyName = "Recipient 3";

            // Set other properties as needed
            message.Subject = "Complex BulkSend Example";
            message.From.Set("from@example.com", "FromMe");
            message.ReplyTo.Email = "replyto@example.com";

            message.CustomHeaders.Add(new CustomHeader("testMessageHeader", "I am a message header"));
            
            message.Metadata.Add("x-mycustommetadata", "I am custom metadata");

            message.Tags.Add("Basic-Complex-Example");

            // Build the Content (Note the %% symbols used to denote the data to be merged)
            var html = new StringBuilder();
            html.AppendLine("<html>");
            html.AppendLine("   <head><title>Complex</title></head>");
            html.AppendLine("   <body>");
            html.AppendLine("       <h1>Merged Data</h1>");
            html.AppendLine("       <p>");
            html.AppendLine("           Motto = <b>%%Motto%%</b> </br>");
            html.AppendLine("           Birthday = <b>%%Birthday%%</b> </br>");
            html.AppendLine("           Age = <b>%%Age%%</b> </br>");
            html.AppendLine("           UpSell = <b>%%UpSell%%</b> </br>");
            html.AppendLine("       </p>");
            html.AppendLine("       </br>");
            html.AppendLine("       <h1>Example of Merge Usage</h1>");
            html.AppendLine("       <p>");
            html.AppendLine("           Our company motto is '<b>%%Motto%%</b>'. </br>");
            html.AppendLine("           Your birthday is <b>%%Birthday%%</b> and you are <b>%%Age%%</b> years old. </br>");
            html.AppendLine("           </br>");
            html.AppendLine("           <b>%%UpSell%%<b>");
            html.AppendLine("       </p>");
            html.AppendLine("   </body>");
            html.AppendLine("</html>");
            message.HtmlBody = html.ToString();

            // Amp Body option
            var amp = new StringBuilder();
            amp.AppendLine("<!doctype html>");
            amp.AppendLine("<html amp4email>");
            amp.AppendLine("   <head><title>Complex AMP Email</title><meta charset=\"utf-8\">");
            amp.AppendLine("  <script async src=\"https://cdn.ampproject.org/v0.js\"></script>");
            amp.AppendLine("  <style amp4email-boilerplate>body{visibility:hidden}</style>");
            amp.AppendLine("  <style amp-custom>");
            amp.AppendLine("    h1 {");
            amp.AppendLine("      margin: 1rem;");
            amp.AppendLine("    }");
            amp.AppendLine("  </style></head>");
            amp.AppendLine("   <body>");
            amp.AppendLine("       <h1>AMP Is Enabled</h1>");
            amp.AppendLine("       <h1>Merged Data</h1>");
            amp.AppendLine("       <p>");
            amp.AppendLine("           Motto = <b>%%Motto%%</b> </br>");
            amp.AppendLine("           Birthday = <b>%%Birthday%%</b> </br>");
            amp.AppendLine("           Age = <b>%%Age%%</b> </br>");
            amp.AppendLine("           UpSell = <b>%%UpSell%%</b> </br>");
            amp.AppendLine("       </p>");
            amp.AppendLine("       </br>");
            amp.AppendLine("       <h1>Example of Merge Usage</h1>");
            amp.AppendLine("       <p>");
            amp.AppendLine("           Our company motto is '<b>%%Motto%%</b>'. </br>");
            amp.AppendLine("           Your birthday is <b>%%Birthday%%</b> and you are <b>%%Age%%</b> years old. </br>");
            amp.AppendLine("           </br>");
            amp.AppendLine("           <b>%%UpSell%%<b>");
            amp.AppendLine("       </p>");
            amp.AppendLine("   </body>");
            amp.AppendLine("</html>");
            message.AmpBody = amp.ToString();

            message.PlainTextBody = Regex.Replace(message.HtmlBody, "<.*?>", string.Empty); 

            // Add an Attachment with a custom header
            var attachment = message.Attachments.AddAsync("bus.png", MimeType.PNG, @".\examples\img\bus.png").Result;
            attachment.CustomHeaders.Add(new CustomHeader("Attachment-Header","I Am A Bus"));
            message.Attachments.Add(attachment);

            // Send the message 
            return client.Send(message);
        }
    }
}
