using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebAPICore.Models;
using System.Collections.Generic;

namespace WebAPICore.IntegrationTest
{
    public class UnitTest1
    {   

        private IWebHostBuilder CreateWebHostBuilder()
        {
            var config = new ConfigurationBuilder().Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseStartup<Startup>();

            return host;
        }

        [Fact]
        public async Task HttpGet_CustomerController_ReturnsSuccess()
        {

            var webHostBuilder = CreateWebHostBuilder();
            var server = new TestServer(webHostBuilder);

            using (var client = server.CreateClient())
            {
                var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "/api/customer/");
                var responseMessage = await client.SendAsync(requestMessage);
                Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
            }
        }

        [Fact]
        public async Task HttpGet_CustomerControllerFiltered_ReturnsSuccess()
        {

            var webHostBuilder = CreateWebHostBuilder();
            var server = new TestServer(webHostBuilder);

            using (var client = server.CreateClient())
            {
                var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "/api/customer/04-01-2017");
                var responseMessage = await client.SendAsync(requestMessage);
                var jsonString = responseMessage.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<Customer>>(jsonString);
                Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
                Assert.Equal(8, model.Count);
            }
        }

        [Fact]
        public async Task HttpGet_CustomerControllerFiltered_ReturnsFail()
        {

            var webHostBuilder = CreateWebHostBuilder();
            var server = new TestServer(webHostBuilder);

            using (var client = server.CreateClient())
            {
                var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "/api/customer/abc");
                var responseMessage = await client.SendAsync(requestMessage);
                Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);
            }
        }
    }
}
