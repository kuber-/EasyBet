using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Models
{
    public class CustomersListDto
    {
        public CustomersListDto()
        {
            Customers = Enumerable.Empty<Customer>();
        }

        public IEnumerable<Customer> Customers;

        public double TotalBetsValue { get; set; }
    }

    public class Customer
    {
        public string Name { get; set; }
        public int BetsCount { get; set; }
        public double BetsAmount { get; set; }
        public bool Risky { get; set; }
    }
}
