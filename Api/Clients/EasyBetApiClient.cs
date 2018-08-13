using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.Extensions.Options;
using Api.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;

namespace Api.Clients
{
    public interface IEasyBetApiClient {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
        Task<IEnumerable<BetDto>> GetBetsAsync();
        Task<IEnumerable<RaceDto>> GetRacesAsync();
    }

    public class EasyBetApiClient : IEasyBetApiClient
    {
        private readonly HttpClient client;
        private readonly string nameQueryValue;

        public EasyBetApiClient(
            IOptions<EasyBetApiClientOptions> options)
        {
            nameQueryValue = options.Value.NameQueryValue;
            client = new HttpClient
            {
                BaseAddress = new Uri(options.Value.Host)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<BetDto>> GetBetsAsync()
        {
            var bets = Enumerable.Empty<BetDto>();
            var path = $"/api/GetBetsV2?name={nameQueryValue}";
            var response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                bets = await response.Content.ReadAsAsync<IEnumerable<BetDto>>();
            }

            return bets;
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var customers = Enumerable.Empty<CustomerDto>();
            var path = $"/api/GetCustomers?name={nameQueryValue}";
            var response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                customers = await response.Content.ReadAsAsync<IEnumerable<CustomerDto>>();
            }

            return customers;
        }

        public async Task<IEnumerable<RaceDto>> GetRacesAsync()
        {
            var races = Enumerable.Empty<RaceDto>();
            var path = $"/api/GetRaces?name={nameQueryValue}";
            var response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                races = await response.Content.ReadAsAsync<IEnumerable<RaceDto>>();
            }

            return races;
        }
    }
}
