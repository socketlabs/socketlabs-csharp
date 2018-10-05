using System.Collections.Generic;
using dotNetCoreExample.Examples.Integration.DataSource.Entity;

namespace dotNetCoreExample.Examples.Integration.DataSource.Repository
{
    internal class CustomerRepository
    {
        //For our example we are using a mock repository class that returns hard-coded data.
        //Normally this class would access a database to retrieve this data.
        public ICollection<Customer> GetCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer{FirstName = "Recipient", LastName = "One", EmailAddress = "recipient1@example.com", FavoriteColor = "Green"},
                new Customer{FirstName = "Recipient", LastName = "Two", EmailAddress = "recipient2@example.com", FavoriteColor = "Red"},
                new Customer{FirstName = "Recipient", LastName = "Three", EmailAddress = "recipient3@example.com", FavoriteColor = "Blue"},
                new Customer{FirstName = "Recipient", LastName = "Four", EmailAddress = "recipient4@example.com", FavoriteColor = "Orange"}
            };

            return customers;
        }
    }
}
