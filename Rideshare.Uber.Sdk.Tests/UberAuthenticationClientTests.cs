using System.Configuration;
using Rideshare.Uber.Sdk.Models;
using Xunit;

namespace Rideshare.Uber.Sdk.Tests
{
    public class UberAuthenticationClientTests
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public UberAuthenticationClientTests()
        {
            this._clientId = ConfigurationManager.AppSettings["ClientId"];
            this._clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        }

        [Fact]
        public void Authorize_ReturnsRedirectUrl()
        {
            var uberClient = new UberAuthenticationClient(_clientId, _clientSecret);

            var response = uberClient.GetAuthorizeUrl();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "OAuth")]
        public async void GetAccessToken_ForValidCode_ReturnsValidAccessToken()
        {
            var uberClient = new UberAuthenticationClient(_clientId, _clientSecret);

            var response = await uberClient.GetAccessTokenAsync("TODO", "https://sandbox-api.uber.com/");

            Assert.NotNull(response);
            Assert.IsType<AccessToken>(response);
            Assert.NotNull(response.Value);
            Assert.NotEmpty(response.Value);
        }

        [Fact(Skip = "OAuth")]
        public async void GetAccessToken_ForInvalidCode_ReturnsNull()
        {
            var uberClient = new UberAuthenticationClient(_clientId, _clientSecret);

            var response = await uberClient.GetAccessTokenAsync("INVALID", "https://sandbox-api.uber.com/");

            Assert.Null(response);
        }

        [Fact(Skip = "OAuth")]
        public async void RefreshAccessToken_ForValidRefreshToken_ReturnsValidAccessToken()
        {
            var uberClient = new UberAuthenticationClient(_clientId, _clientSecret);

            var response = await uberClient.RefreshAccessTokenAsync("TODO", "https://sandbox-api.uber.com/");

            Assert.NotNull(response);
            Assert.IsType<AccessToken>(response);
            Assert.NotEmpty(response.Value);
        }

        [Fact(Skip = "OAuth")]
        public async void RefreshAccessToken_ForInvalidefreshToken_ReturnsNull()
        {
            var uberClient = new UberAuthenticationClient(_clientId, _clientSecret);

            var response = await uberClient.RefreshAccessTokenAsync("INVALID", "https://sandbox-api.uber.com/");

            Assert.Null(response);
        }

        [Fact(Skip = "OAuth")]
        public async void RevokeAccessToken_ForValidCode_ReturnsTrue()
        {
            var uberClient = new UberAuthenticationClient(_clientId, _clientSecret);

            var response = await uberClient.RevokeAccessTokenAsync("...");

            Assert.True(response);
        }

        [Fact(Skip = "OAuth")]
        public async void RevokeAccessToken_ForInvalidCode_ReturnsFalse()
        {
            var uberClient = new UberAuthenticationClient(_clientId, _clientSecret);

            var response = await uberClient.RevokeAccessTokenAsync("...");

            Assert.False(response);
        }
    }
}
