using Xunit;
using System;
using Moq;
using Api.Services;
using Api.Clients;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Api.Models;

namespace Tests.Services
{
    public class CustomersServiceShould
    {
        private readonly Mock<IEasyBetApiClient> mockApiClient = new Mock<IEasyBetApiClient>();
        private readonly CustomersService customersService;

        public CustomersServiceShould()
        {
            customersService = new CustomersService(mockApiClient.Object);
            mockApiClient.Setup(x => x.GetCustomersAsync()).ReturnsAsync(Customers);
            mockApiClient.Setup(x => x.GetBetsAsync()).ReturnsAsync(Bets);
        }

        [Fact]
        public async Task InvokeGetCustomers() {
            await customersService.GetCustomersAsync();
            mockApiClient.Verify(x => x.GetCustomersAsync(), Times.Once);
        }

        [Fact]
        public async Task InvokeGetBets() {
            await customersService.GetCustomersAsync();
            mockApiClient.Verify(x => x.GetBetsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCustomersWithNoBets() {
            var result = await customersService.GetCustomersAsync();
            Assert.Equal(result.Customers.Count(), Customers.Count);
        }

        [Fact]
        public async Task GetCustomersTotalBetAmount() {
            var result = await customersService.GetCustomersAsync();
            Assert.Equal(650, result.TotalBetsValue);
        }

        [Fact]
        public async Task GetCustomersRiskProfile() {
            var result = await customersService.GetCustomersAsync();
            Assert.Equal(1, result.Customers.Where(x => x.Risky).Count());
        }

        [Fact]
        public async Task GetTotalBetsCounts() {
            var result = await customersService.GetCustomersAsync();
            Assert.Equal(3, result.Customers.First(x => x.Name == "Joe").BetsCount);
            Assert.Equal(2, result.Customers.First(x => x.Name == "Dow").BetsCount);
            Assert.Equal(0, result.Customers.First(x => x.Name == "John").BetsCount);
        }

        [Fact]
        public async Task GetTotalBetsAmounts()
        {
            var result = await customersService.GetCustomersAsync();
            Assert.Equal(100, result.Customers.First(x => x.Name == "Joe").BetsAmount);
            Assert.Equal(550, result.Customers.First(x => x.Name == "Dow").BetsAmount);
            Assert.Equal(0, result.Customers.First(x => x.Name == "John").BetsAmount);
        }

        public static List<CustomerDto> Customers = new List<CustomerDto> {
            new CustomerDto { Id = 1, Name = "Joe" },
            new CustomerDto { Id = 2, Name = "Dow" },
            new CustomerDto { Id = 3, Name = "John" },
        };

        public static List<BetDto> Bets = new List<BetDto>
        {
            new BetDto {
                CustomerId = 1,
                RaceId = 1,
                HorseId = 11,
                Stake = 50
            },
            new BetDto {
                CustomerId = 1,
                RaceId = 2,
                HorseId = 21,
                Stake = 20
            },
            new BetDto {
                CustomerId = 1,
                RaceId = 3,
                HorseId = 32,
                Stake = 30
            },
            new BetDto {
                CustomerId = 2,
                RaceId = 4,
                HorseId = 42,
                Stake = 250
            },
            new BetDto {
                CustomerId = 2,
                RaceId = 5,
                HorseId = 55,
                Stake = 300
            },
        };

        public static TheoryData<List<CustomerDto>, List<BetDto>, int> BetsData =
            new TheoryData<List<CustomerDto>, List<BetDto>, int>
            {
                {
                Customers, new List<BetDto>(), 1
                }
            };
    }
}
