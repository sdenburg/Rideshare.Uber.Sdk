using System.Linq;
using Rideshare.Uber.Sdk.Models;
using Xunit;

namespace Rideshare.Uber.Sdk.Tests
{
    public class ServerAuthenticatedUberRiderServiceTests
    {
        private readonly string _serverToken;
        private readonly string _sandboxUrl = "https://sandbox-api.uber.com";

        public ServerAuthenticatedUberRiderServiceTests()
        {
            var configuration = ConfigurationLoader.GetConfiguration();
            
            this._serverToken = configuration.AppSettings.Settings["ServerToken"].Value;
        }

        #region Products

        [Fact]
        public async void GetProducts_ForValidParameters_ReturnsListOfProducts()
        {
            var uberClient = new ServerAuthenticatedUberRiderService(_serverToken, _sandboxUrl);

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

        [Fact]
        public async void GetProducts_ForInvalidParameters_ReturnsEmptyList()
        {
            var uberClient = new ServerAuthenticatedUberRiderService(_serverToken, _sandboxUrl);

            var response = await uberClient.GetProductsAsync(
                TestLocations.SouthPoleLatitude, TestLocations.SouthPoleLongitude);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<ProductCollection>(response.Data);
            Assert.NotNull(response.Data.Products);
            Assert.Empty(response.Data.Products);
        }

        [Fact(Skip = "TODO")]
        public async void GetProductDetails_ForValidParameters_ReturnsListOfProducts()
        {
            var uberClient = new ServerAuthenticatedUberRiderService(_serverToken, _sandboxUrl);

            var response = await uberClient.GetProductDetailsAsync("TODO");

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<ProductCollection>(response.Data);
            Assert.NotNull(response.Data.Products);
            Assert.NotEmpty(response.Data.Products);
            Assert.NotNull(response.Data.Products[0].DisplayName);
            Assert.NotEmpty(response.Data.Products[0].DisplayName);
        }

        #endregion

        #region Estimates

        #region Price

        [Fact]
        public async void GetPriceEstimate_ForValidParameters_ReturnsListOfPriceEstimates()
        {
            var uberClient = new ServerAuthenticatedUberRiderService(_serverToken, _sandboxUrl);

            var response = await uberClient.GetPriceEstimateAsync(
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude,
                TestLocations.CapitalLatitude, TestLocations.CapitalLongitude);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<PriceEstimateCollection>(response.Data);
            Assert.NotNull(response.Data.PriceEstimates);
            Assert.NotEmpty(response.Data.PriceEstimates);
            Assert.NotNull(response.Data.PriceEstimates[0].DisplayName);
            Assert.NotEmpty(response.Data.PriceEstimates[0].DisplayName);
        }

        [Fact]
        public async void GetPriceEstimate_ForInvalidParameters_ReturnsEmptyList()
        {
            var uberClient = new ServerAuthenticatedUberRiderService(_serverToken, _sandboxUrl);

            var response = await uberClient.GetPriceEstimateAsync(
                TestLocations.SouthPoleLatitude, TestLocations.SouthPoleLongitude,
                TestLocations.SouthPoleLatitude, TestLocations.SouthPoleLongitude);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<PriceEstimateCollection>(response.Data);
            Assert.NotNull(response.Data.PriceEstimates);
            Assert.Empty(response.Data.PriceEstimates);
        }

        #endregion

        #region Time

        [Fact]
        public async void GetTimeEstimate_ForValidDefaultParameters_ReturnsListOfPriceEstimates()
        {
            var uberClient = new ServerAuthenticatedUberRiderService(_serverToken, _sandboxUrl);

            var response = await uberClient.GetTimeEstimateAsync(
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<TimeEstimateCollection>(response.Data);
            Assert.NotNull(response.Data.TimeEstimates);
            Assert.NotEmpty(response.Data.TimeEstimates);
            Assert.NotNull(response.Data.TimeEstimates[0].ProductId);
            Assert.NotEmpty(response.Data.TimeEstimates[0].ProductId);
        }

        [Fact]
        public async void GetTimeEstimate_ForInvalidDefaultParameters_ReturnsEmptyList()
        {
            var uberClient = new ServerAuthenticatedUberRiderService(_serverToken, _sandboxUrl);

            var response = await uberClient.GetTimeEstimateAsync(
                TestLocations.SouthPoleLatitude, TestLocations.SouthPoleLongitude);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<TimeEstimateCollection>(response.Data);
            Assert.NotNull(response.Data.TimeEstimates);
            Assert.Empty(response.Data.TimeEstimates);
        }

        [Fact(Skip = "TODO")]
        public async void GetTimeEstimate_ForValidProductId_ReturnsListOfPriceEstimates()
        {
            var uberClient = new ServerAuthenticatedUberRiderService(_serverToken, _sandboxUrl);

            var response = await uberClient.GetTimeEstimateAsync(
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude, "TODO");

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<TimeEstimateCollection>(response.Data);
            Assert.NotNull(response.Data.TimeEstimates);
            Assert.NotEmpty(response.Data.TimeEstimates);
            Assert.NotNull(response.Data.TimeEstimates[0].ProductId);
            Assert.NotEmpty(response.Data.TimeEstimates[0].ProductId);
        }

        [Fact]
        public async void GetTimeEstimate_ForInvalidProductId_ReturnsEmptyList()
        {
            var uberClient = new ServerAuthenticatedUberRiderService(_serverToken, _sandboxUrl);

            var response = await uberClient.GetTimeEstimateAsync(
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude, "INVALID");

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<TimeEstimateCollection>(response.Data);
            Assert.NotNull(response.Data.TimeEstimates);
            Assert.Empty(response.Data.TimeEstimates);
        }

        #endregion

        #endregion
    }
}