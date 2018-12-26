using System.Configuration;
using Rideshare.Uber.Sdk.Models;
using Xunit;

namespace Rideshare.Uber.Sdk.Tests
{
    public class UberClientTests
    {
        private readonly string _clientToken;
        private readonly string _serverToken;
        private readonly string _sandboxUrl = "https://sandbox-api.uber.com";

        public UberClientTests()
        {
            _clientToken = ConfigurationManager.AppSettings["ClientToken"];
            _serverToken = ConfigurationManager.AppSettings["ServerToken"];
        }

        [Fact]
        public async void GetProducts_ForValidParameters_ReturnsListOfProducts()
        {
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

            var response = await uberClient.GetProductsAsync(
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<ProductCollection>(response.Data);
            Assert.NotNull(response.Data.Products);
            Assert.NotEmpty(response.Data.Products);
            Assert.NotNull(response.Data.Products[0].DisplayName);
            Assert.NotEmpty(response.Data.Products[0].DisplayName);
        }
    }
}
