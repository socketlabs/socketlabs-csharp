using dotNetCoreExample.Examples.Integration.DataSource.Repository;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;

namespace dotNetCoreExample.Examples.Bulk
{
    class BulkSendFromDataSourceWithMerge : IExample
    {
        public SendResponse RunExample()
        {
            var client = new SocketLabsClient(ExampleConfig.ServerId, ExampleConfig.ApiKey)
            {
                EndpointUrl = ExampleConfig.TargetApi
            };

            //Retrieve data from the datasource
            var customerRepository = new CustomerRepository();
            var customers = customerRepository.GetCustomers();

            //Build the message
            var message = new BulkMessage();

            message.Subject = "Hello %%FirstName%%";
            message.PlainTextBody = "Hello %%FirstName%% %%LastName%%. Is your favorite color still %%FavoriteColor%%?";
            message.From.Email = "from@example.com";
            message.ReplyTo.Email = "replyto@example.com";
            
            //Merge in the customers from the datasource
            foreach (var customer in customers)
            {
                var recipient = new BulkRecipient(customer.EmailAddress);
                recipient.MergeData.Add("FirstName", customer.FirstName);
                recipient.MergeData.Add("LastName", customer.LastName);
                recipient.MergeData.Add("FavoriteColor", customer.FavoriteColor);

                message.To.Add(recipient);
            }

            return client.Send(message);
        }
    }
}
