using System.Configuration;
using System.Linq;
using System.Reflection;
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
            var fileMap = new ExeConfigurationFileMap()
            {
                ExeConfigFilename = $"./{Assembly.GetExecutingAssembly().GetName().Name}.dll.config"
            };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            // this._clientToken = configuration.AppSettings.Settings["ClientToken"].Value;
            this._serverToken = configuration.AppSettings.Settings["ServerToken"].Value;
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

        [Fact]
        public async void GetProducts_ForInvalidParameters_ReturnsEmptyList()
        {
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

            var response = await uberClient.GetProductsAsync(
                TestLocations.SouthPoleLatitude, TestLocations.SouthPoleLongitude);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<ProductCollection>(response.Data);
            Assert.NotNull(response.Data.Products);
            Assert.Empty(response.Data.Products);
        }

        [Fact]
        public async void GetPriceEstimate_ForValidParameters_ReturnsListOfPriceEstimates()
        {
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

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
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

            var response = await uberClient.GetPriceEstimateAsync(
                TestLocations.SouthPoleLatitude, TestLocations.SouthPoleLongitude,
                TestLocations.SouthPoleLatitude, TestLocations.SouthPoleLongitude);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<PriceEstimateCollection>(response.Data);
            Assert.NotNull(response.Data.PriceEstimates);
            Assert.Empty(response.Data.PriceEstimates);
        }

        [Fact]
        public async void GetTimeEstimate_ForValidDefaultParameters_ReturnsListOfPriceEstimates()
        {
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

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
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

            var response = await uberClient.GetTimeEstimateAsync(
                TestLocations.SouthPoleLatitude, TestLocations.SouthPoleLongitude);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<TimeEstimateCollection>(response.Data);
            Assert.NotNull(response.Data.TimeEstimates);
            Assert.Empty(response.Data.TimeEstimates);
            Assert.NotNull(response.Data.TimeEstimates[0].ProductId);
            Assert.NotEmpty(response.Data.TimeEstimates[0].ProductId);
        }

        [Fact]
        public async void GetTimeEstimate_ForValidCustomerId_ReturnsListOfPriceEstimates()
        {
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

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
        public async void GetTimeEstimate_ForInvalidCustomerId_ReturnsListOfPriceEstimates()
        {
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

            var response = await uberClient.GetTimeEstimateAsync(
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude, "INVALID");

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<TimeEstimateCollection>(response.Data);
            Assert.NotNull(response.Data.TimeEstimates);
            Assert.NotEmpty(response.Data.TimeEstimates);
            Assert.NotNull(response.Data.TimeEstimates[0].ProductId);
            Assert.NotEmpty(response.Data.TimeEstimates[0].ProductId);
        }

        [Fact]
        public async void GetTimeEstimate_ForValidProductId_ReturnsListOfPriceEstimates()
        {
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

            var response = await uberClient.GetTimeEstimateAsync(
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude, "TODO", "TODO");

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<TimeEstimateCollection>(response.Data);
            Assert.NotNull(response.Data.TimeEstimates);
            Assert.NotEmpty(response.Data.TimeEstimates);
            Assert.NotNull(response.Data.TimeEstimates[0].ProductId);
            Assert.NotEmpty(response.Data.TimeEstimates[0].ProductId);
        }

        [Fact]
        public async void GetTimeEstimate_ForInvalidProductId_ReturnsListOfPriceEstimates()
        {
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

            var response = await uberClient.GetTimeEstimateAsync(
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude, "TODO", "INVALID");

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<TimeEstimateCollection>(response.Data);
            Assert.NotNull(response.Data.TimeEstimates);
            Assert.NotEmpty(response.Data.TimeEstimates);
            Assert.NotNull(response.Data.TimeEstimates[0].ProductId);
            Assert.NotEmpty(response.Data.TimeEstimates[0].ProductId);
        }

        [Fact]
        public async void GetPromotion_ForValidParameters_ReturnsPromotion()
        {
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

            var response = await uberClient.GetPromotionAsync(
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude,
                TestLocations.CapitalLatitude, TestLocations.CapitalLongitude);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<Promotion>(response.Data);
        }

        [Fact]
        public async void GetPromotion_ForInvalidParameters_ReturnsError()
        {
            var uberClient = new UberClient(AccessTokenType.Server, _serverToken, _sandboxUrl);

            var response = await uberClient.GetPromotionAsync(
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude,
                TestLocations.SouthPoleLatitude, TestLocations.SouthPoleLongitude);

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotNull(response.Error);
        }

        [Fact]
        public async void GetUserActivity_ForValidParameters_ReturnsUserUserActivity()
        {
            var uberClient = new UberClient(AccessTokenType.Client, _clientToken, _sandboxUrl);

            var response = await uberClient.GetUserActivityAsync(0, 10);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<Promotion>(response.Data);
        }

        [Fact]
        public async void GetUserActivity_ForInvalidParameters_ReturnsError()
        {
            var uberClient = new UberClient(AccessTokenType.Client, _clientToken, _sandboxUrl);

            var response = await uberClient.GetUserActivityAsync(0, -1);

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotNull(response.Error);
        }

        [Fact]
        public async void GetUserProfile_ForValidToken_ReturnsUserUserActivity()
        {
            var uberClient = new UberClient(AccessTokenType.Client, _clientToken, _sandboxUrl);

            var response = await uberClient.GetUserProfileAsync();

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<Promotion>(response.Data);
        }

        [Fact]
        public async void GetUserProfile_ForInvalidToken_ReturnsError()
        {
            var uberClient = new UberClient(AccessTokenType.Client, _clientToken, _sandboxUrl);

            var response = await uberClient.GetUserProfileAsync();

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotNull(response.Error);
        }

        [Fact]
        public async void Request_ForValidParameters_ReturnsRequest()
        {
            var uberClient = new UberClient(AccessTokenType.Client, _clientToken, _sandboxUrl);

            var response = await uberClient.RequestAsync(
                "893b94af-ca9d-4f0f-9201-6d426cedaa5c",
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude,
                TestLocations.CapitalLatitude, TestLocations.CapitalLongitude);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<Request>(response.Data);
        }

        [Fact]
        public async void Request_ForInvalidParameters_ReturnsError()
        {
            var uberClient = new UberClient(AccessTokenType.Client, _clientToken, _sandboxUrl);

            var response = await uberClient.RequestAsync(
                "INVALID",
                TestLocations.WhiteHouseLatitude, TestLocations.WhiteHouseLongitude,
                TestLocations.SouthPoleLatitude, TestLocations.SouthPoleLongitude);

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotNull(response.Error);
        }

        [Fact]
        public async void GetRequestDetails_ForValidParameters_ReturnsRequestDetails()
        {
            var uberClient = new UberClient(AccessTokenType.Client, _clientToken, _sandboxUrl);

            var allRequests = await uberClient.GetUserActivityAsync(0, 50);
            var response = await uberClient.GetRequestDetailsAsync(allRequests.Data.History.First().Id);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.IsType<RequestDetails>(response.Data);
        }

        [Fact]
        public async void GetRequestDetails_ForValidParameters_ReturnsError()
        {
            var uberClient = new UberClient(AccessTokenType.Client, _clientToken, _sandboxUrl);

            var response = await uberClient.GetRequestDetailsAsync("INVALID");

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotNull(response.Error);
        }
    }
}
