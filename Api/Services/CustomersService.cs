using System;
using System.Collections.Generic;
using Api.Models;
using Api.Clients;
using System.Threading.Tasks;
using System.Linq;

namespace Api.Services
{
    public interface ICustomersService {
        Task<CustomersListDto> GetCustomersAsync();
    }

    public class CustomersService : ICustomersService
    {
        private readonly IEasyBetApiClient client;
        private static double RiskyBetThreshold = 200;

        public CustomersService(IEasyBetApiClient client)
        {
            this.client = client;
        }

        public async Task<CustomersListDto> GetCustomersAsync()
        {
            var customersList = new List<Customer>();
            var customers = await client.GetCustomersAsync();
            var bets = await client.GetBetsAsync();
            var customersDict = customers.ToDictionary(x => x.Id, x => x.Name);

            // group by customersid to aggregate and get bet count and bet amount.
            var groupedBets = bets.GroupBy(x => x.CustomerId);
            foreach(var group in groupedBets) {
                var betAmount = group.Sum(x => x.Stake);
                var customer = new Customer
                {
                    Name = customersDict[group.Key],
                    BetsAmount = betAmount,
                    BetsCount = group.Count(),
                    Risky = betAmount > RiskyBetThreshold
                };
                customersList.Add(customer);
                customersDict.Remove(group.Key);
            }

            // Add remaining customers
            foreach(var (id, name) in customersDict) {
                customersList.Add(new Customer
                {
                    Name = name,
                    BetsAmount = 0,
                    BetsCount = 0,
                    Risky = false,
                });
            }

            return new CustomersListDto
            {
                Customers = customersList.OrderBy(x => x.Name),
                TotalBetsValue = customersList.Sum(x => x.BetsAmount)
            };
        }
    }
}
