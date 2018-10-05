using SocketLabs.InjectionApi;

namespace dotNetCoreExample.Examples.QuickSend
{
    public class QuickSend
    {
        public void RunExampleHtmlAndText()
        {
            var response = SocketLabsClient.QuickSend(
                ExampleConfig.ServerId,
                ExampleConfig.ApiKey,
                "recipient@example.com",
                "from@example.com",
                "Lorem Ipsum",
                "<html>Lorem Ipsum</html>",
                "Lorem Ipsum"
            );
        }

        public void RunExampleHtml()
        {
            var response = SocketLabsClient.QuickSend(
                ExampleConfig.ServerId,
                ExampleConfig.ApiKey,
                "recipient@example.com",
                "from@example.com",
                "Lorem Ipsum",
                "<html>Lorem Ipsum</html>",
                true
            );
        }

        public void RunExampleText()
        {
            var response = SocketLabsClient.QuickSend(
                ExampleConfig.ServerId,
                ExampleConfig.ApiKey,
                "recipient@example.com",
                "from@example.com",
                "Lorem Ipsum",
                "Lorem Ipsum",
                false
            );
        }
    }
}
