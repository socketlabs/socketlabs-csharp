The SocketLabs Email Delivery C# library

SocketLabs is a cloud-based email delivery platform built to help you send 
your marketing and transactional email from one platform. SocketLabs provides
optimized email delivery at scale, real-time tracking and analytics, 
reliable Email APIs for developers, and a suite of Email Marketing Tools 
that will make sending your next email campaign a breeze.

* How do I create a SocketLabs account? *
You can sign up for a free account here
https://socketlabs.com/signup_form/?plan=Free.

* Help & Support *
Visit our Support System if you have any questions, the 
SocketLabs Support Team is happy to help — https://support.socketlabs.com

* Getting Started *

Obtaining your API Key and SocketLabs ServerId number
In order to get started, you'll need to enable the Injection API feature in the [SocketLabs Control Panel](https://cp.socketlabs.com). 
Once logged in, navigate to your SocketLabs server's dashboard (if you only have one server on your account you'll be taken here immediately after logging in). 
Make note of your 4 or 5 digit ServerId number, as you'll need this along with 
your API key in order to use the Injection API. 

To enable the Injection API, click on the "For Developers" dropdown on the top-level navigation, then choose the "Configure HTTP Injection API" option. 
Once here, you can enable the feature by choosing the "Enabled" option in the
dropdown. Enabling the feature will also generate your API key, which you'll 
need (along with your ServerId) to start using the API. Be sure to click the 
"Update" button to save your changes once you are finished.

* Quick Send *

The `QuickSend()` method is a static method that allows you to quickly and easily send a message to a single recipient without the need for any setup code or even instantiating a client!


using SocketLabs.InjectionApi;

SocketLabsClient.QuickSend(
                000001, //Your SocketLabs ServerId
                "YOUR-API-KEY", //Your Injection API Key
                "recipient@example.com", //The To address for your message
                "from@example.com", //The From address for your message
                "Lorem Ipsum", //The Subject line for your message
                "<html>Lorem Ipsum</html>", //The HTML content for your message
                "Lorem Ipsum" //The plaintext content for your message
            );


* Basic Message *

A basic message is an email message like you'd send from a personal email client such as Outlook. 
A basic message can have many recipients, including multiple To addresses, CC addresses, and even BCC addresses. 
You can also send a file attachment in a basic message.


using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

var client = new SocketLabsClient(000001, "YOUR-API-KEY"); //Your SocketLabs ServerId and Injection API key

var message = new BasicMessage();

message.Subject = "Sending A BasicMessage";
message.HtmlBody = "<html>This is the Html Body of my message.</html>";
message.PlainTextBody = "This is the Plain Text Body of my message.";

message.From.Email = "from@example.com";

//A basic message supports up to 50 recipients and supports several different ways to add recipients
message.To.Add("recipient1@example.com"); //Add a To address by passing the email address
message.Cc.Add("recipient2@example.com", "Recipient #2"); //Add a CC address by passing the email address and a friendly name
message.Bcc.Add(new EmailAddress("recipient3@example.com")); //Add a BCC address by passing an EmailAddress object

var response = client.Send(message);

