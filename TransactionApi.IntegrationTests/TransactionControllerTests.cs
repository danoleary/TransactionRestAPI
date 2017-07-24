using System;
using Xunit;
using TransactionApi;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using TransactionApi.Models;
using Newtonsoft.Json;
using System.Text;

namespace TransactionApi.IntegrationTests
{
    public class TransactionControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TransactionControllerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task transaction_can_be_added()
        {
            //arrange
            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                Description  = "some transaction",
                TransactionAmount = 1.5M,
                CurrencyCode = "GBP",
                Merchant = " amerchant",
            };

            // Act
            var stringContent = new StringContent(JsonConvert.SerializeObject(transaction),
                                    Encoding.UTF8, 
                                    "application/json");
            var createResponse = await _client.PostAsync("/api/transaction", stringContent);
            createResponse.EnsureSuccessStatusCode();
        }
    }
}
