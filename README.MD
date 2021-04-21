[![SocketLabs](https://static.socketlabs.com/logos/logo-dark-326x64.png)](https://www.socketlabs.com/developers)
# [![Twitter Follow](https://img.shields.io/twitter/follow/socketlabs.svg?style=social&label=Follow)](https://twitter.com/socketlabs) [![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](./LICENSE) [![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](https://github.com/socketlabs/socketlabs-csharp/blob/master/CONTRIBUTING.md)
<!--
[![GitHub contributors](https://img.shields.io/github/contributors/socketlabs/csharp-socketlabs.svg)](https://github.com/socketlabs/csharp-socketlabs/graphs/contributors)
-->

The SocketLabs Email Delivery C# library allows you to easily send email messages via the [SocketLabs Injection API](https://www.socketlabs.com/docs/inject/).  The library makes it easy to build and send any type of message supported by the API, from a simple message to a single recipient all the way to a complex bulk message sent to a group of recipients with unique merge data per recipient.

# Table of Contents
* [Prerequisites and Installation](#prerequisites-and-installation)
* [Getting Started](#getting-started)
* [Managing API Keys](#managing-api-keys)
* [Examples and Use Cases](#examples-and-use-cases)
* [Version](#version)
* [License](#license)


<a name="prerequisites-and-installation"></a>
# Prerequisites and Installation
## Prerequisites
* A supported .NET version
  * .NET version 4.5 or higher
  * .NET Core 1.0 or higher
  * .NET Standard 1.3 or higher
* A SocketLabs account. If you don't have one yet, you can [sign up for a free account](https://signup.socketlabs.com/step-1?plan=free) to get started.

## Installation
For most uses we recommend installing the SocketLabs.EmailDelivery package via Nuget. If you have the [Nuget Package Manager](https://www.nuget.org/) installed already, you can add the latest version of the package with the following command:

```
PM> Install-Package SocketLabs.EmailDelivery
```

.NET CLI users can also use the following command:

```
> dotnet add package SocketLabs.EmailDelivery
```

Alternately, you can simply [clone this repository](https://github.com/socketlabs/socketlabs-csharp.git) directly to include the source code in your project.

**Note for Visual Studio 2012 and earlier users**: Due to an issue with dependency requirements, the package manager in VS2012
and earlier does not support installation of the current nuget package. In order to install the package directly, all you need to do
is download the latest release binaries located at [https://github.com/socketlabs/socketlabs-csharp/releases/](https://github.com/socketlabs/socketlabs-csharp/releases/).
Once you download the zip file, extract the files, and add the dll's from ```the lib\net45``` directory as references.
You can do this from the project menu by clicking "Add Reference" then going to the browse option and picking the dll's from there.

<a name="getting-started"></a>
# Getting Started
## Obtaining your API Key and SocketLabs ServerId number
In order to get started, you'll need to enable the Injection API feature in the [SocketLabs Control Panel](https://cp.socketlabs.com).
Once logged in, navigate to your SocketLabs server's dashboard (if you only have one server on your account you'll be taken here immediately after logging in).
Make note of your 4 or 5 digit ServerId number, as you'll need this along with
your API key in order to use the Injection API.

To enable the Injection API, click on the "For Developers" dropdown on the top-level navigation, then choose the "Configure HTTP Injection API" option.
Once here, you can enable the feature by choosing the "Enabled" option in the
dropdown. Enabling the feature will also generate your API key, which you'll
need (along with your ServerId) to start using the API. Be sure to click the
"Update" button to save your changes once you are finished.

## Quick Send
The `QuickSend()` method is a static method that allows you to quickly and easily send a message to a single recipient without the need for any setup code or even instantiating a client!

```C#
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
```

## Basic Message
A basic message is an email message like you'd send from a personal email client such as Outlook.
A basic message can have many recipients, including multiple To addresses, CC addresses, and even BCC addresses.
You can also send a file attachment in a basic message.

```C#
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
```

## Bulk Message
A bulk message usually contains a single recipient per message
and is generally used to send the same content to many recipients,
optionally customizing the message via the use of MergeData.
For more information about using Merge data, please see the [Injection API documentation](https://www.socketlabs.com/docs/inject/).
```C#
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

var client = new SocketLabsClient(000001, "YOUR-API-KEY"); //Your SocketLabs ServerId and Injection API key

var message = new BulkMessage();

message.PlainTextBody = "This is the body of my message sent to %%Name%%";
message.HtmlBody = "<html>This is the HtmlBody of my message sent to %%Name%%</html>";
message.Subject = "Sending a test message";
message.From.Email = "from@example.com";

var recipient1 = message.To.Add("recipient1@example.com");
recipient1.MergeData.Add("Name","Recipient1");

var recipient2 = message.To.Add("recipient2@example.com");
recipient2.MergeData.Add("Name","Recipient2");

var response = client.Send(message);
```

<a name="managing-api-keys"></a>
## Managing API Keys
For ease of demonstration, many of our examples include the ServerId and API
key directly in our code sample. Generally it is not considered a good practice
to store sensitive information like this directly in your code. Depending on
your project type, we recommend either storing your credentials in an `app.config` or a `web.config` file, or using Environment Variables. For more
information please see:
* [Using web.config](https://docs.microsoft.com/en-us/aspnet/identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure)
* [Using Environment Variables](https://docs.microsoft.com/en-us/dotnet/api/system.environment.getenvironmentvariable)


<a name="examples-and-use-cases"></a>
# Examples and Use Cases
In order to demonstrate the many possible use cases for the SDK, we've provided
an assortment of code examples. These examples demonstrate many different
features available to the Injection API and SDK, including using templates
created in the [SocketLabs Email Designer](https://www.socketlabs.com/blog/introducing-new-email-designer/), custom email headers, sending
attachments, sending content that is stored in an HTML file, advanced bulk
merging, and even pulling recipients from a datasource.

### [Basic send from SocketLabs Template](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Basic/BasicSendWithApiTemplate.cs)
This example demonstrates the sending of a piece of content that was created in the
SocketLabs Email Designer. This is also known as the [API Templates](https://www.socketlabs.com/blog/introducing-api-templates/) feature.

### [Basic send from HTML file](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Basic/BasicSendFromHtmlFile.cs)
This example demonstrates how to read in your HTML content from an HTML file
rather than passing in a string directly.

### [Basic send with file attachment](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Basic/BasicSendWithAttachment.cs)
This example demonstrates how to add a file attachment to your message.

### [Basic send with embedded image](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Basic/BasicSendWithEmbeddedImage.cs)
This example demonstrates how to embed an image in your message.

### [Basic send with specified character set](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Basic/BasicSendWithAsciiCharset.cs)
This example demonstrates sending with a specific character set.

### [Basic send with custom email headers](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Basic/BasicSendWithCustomHeaders.cs)
This example demonstrates how to add custom headers to your email message.

### [Basic send with a web proxy](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Basic/BasicSendWithProxy.cs)
This example demonstrates how to use a proxy with your HTTP client.

### [Basic send with retry enabled](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Basic/BasicSendWithRetry.cs)
This example demonstrates how to use the retry logic with your HTTP client.

### [Basic send complex example](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Basic/BasicComplexExample.cs)
This example demonstrates many features of the Basic Send, including adding multiple recipients, adding message and mailing id's, and adding an embedded image.

### [Basic send with Amp ](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Basic/BasicSendWithApiTemplate.cs)
This example demonstrates how to send a basic message with an AMP Html body.
For more information about AMP please see [AMP Project](https://amp.dev/documentation/)

### [Bulk send with multiple recipients](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Bulk/BulkSend.cs)
This example demonstrates how to send a bulk message to multiple recipients.

### [Bulk send with merge data](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Bulk/BulkSendWithMergeData.cs)
This example demonstrates how to send a bulk message to multiple recipients with
unique merge data per recipient.

### [Bulk send with complex merge including attachments](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Bulk/BulkSendComplexExample.cs)
This example demonstrates many features of the `BulkMessage()`, including
adding multiple recipients, merge data, and adding an attachment.

### [Bulk send with recipients pulled from a datasource](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Bulk/BulkSendFromDataSourceWithMerge.cs)
This example uses a mock repository class to demonstrate how you would pull
your recipients from a database and create a bulk mailing with merge data.

### [Bulk send with Ascii charset and special characters](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Bulk/BulkSendWithAsciiCharsetMergeData.cs)
This example demonstrates how to send a bulk message with a specified character
set and special characters.

### [Bulk send with Amp ](https://github.com/socketlabs/socketlabs-csharp/blob/master/Example%20Projects/dotNetCoreExample/Examples/Bulk/BulkSendWithAmpBody.cs)
This example demonstrates how to send a bulk message with an AMP Html body.
For more information about AMP please see [AMP Project](https://amp.dev/documentation/)

<a name="version"></a>
# Version
* 1.2.3 - Revert request timeout override. To set the timeout please pass your HttpClient instance via the constructor.
* 1.2.2 - Adding optional retry logic for Http requests. If configured, the request will retry when certain 500 errors occur (500, 502, 503, 504)
* 1.2.1 - Adding request timeout value on the client for Http requests
* 1.2.0 - Can now pass in instance of HTTP client, adds .NET 5.0 support
* 1.1.0 - Adds Amp Html Support
* 1.0.0 - Initial Release

<a name="license"></a>
# License
The SocketLabs.EmailDelivery library and all associated code, including any code samples, are [MIT Licensed](https://github.com/socketlabs/socketlabs-csharp/blob/master/LICENSE.MD).
