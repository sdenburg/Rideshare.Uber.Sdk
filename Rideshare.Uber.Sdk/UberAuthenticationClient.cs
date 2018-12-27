using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Rideshare.Uber.Sdk.Models;

namespace Rideshare.Uber.Sdk
{
    public class UberAuthenticationClient
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        /// <summary>
        /// Initialises a new <see cref="UberAuthenticationClient"/>
        /// </summary>
        /// <param name="clientId">
        /// The client ID, this can be found at https://developer.uber.com/apps/
        /// </param>
        /// <param name="clientSecret">
        /// The client secret, this can be found at https://developer.uber.com/apps/
        /// </param>
        public UberAuthenticationClient(string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentException("Parameter is required", nameof(clientId));
            if (string.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentException("Parameter is required", nameof(clientSecret));

            this._clientId = clientId;
            this._clientSecret = clientSecret;
        }

        /// <summary>
        /// Generates an Uber OAuth authorization URL based on scopes, state and redirect.
        /// </summary>
        /// <param name="scopes">
        /// Space delimited list of grant scopes you would like to have permission to access on behalf of the user.
        /// If none is provided the default is the set selected in your application’s dashboard.
        /// It is invalid to provide no scopes and have none selected in the dashboard.
        /// </param>
        /// <param name="state">
        /// State which will be passed back to you to prevent tampering.
        /// </param>
        /// <param name="redirectUri">
        /// The URI we will redirect back to after an authorization by the resource owner.
        /// If none is provided the default is the first redirect URI provided in the dashboard.
        /// </param>
        /// <returns>
        /// Returns the OAuth authorization URL.
        /// </returns>
        public string GetAuthorizeUrl(List<string> scopes = null, string state = null, string redirectUri = null)
        {
            var authorizeUrl = $"https://login.uber.com/oauth/v2/authorize?client_id={this._clientId}&response_type=code";

            if (scopes != null && scopes.Any())
            {
                authorizeUrl += $"&scope={string.Join(" ", scopes)}";
            }

            if (!string.IsNullOrWhiteSpace(state))
            {
                authorizeUrl += $"&state={state}";
            }

            if (!string.IsNullOrWhiteSpace(redirectUri))
            {
                authorizeUrl += $"&redirect_uri={HttpUtility.UrlEncode(redirectUri)}";
            }

            return authorizeUrl;
        }

        /// <summary>
        /// Exchange this authorization code for an AccessToken, allowing requests to be mande on behalf of a user.
        /// </summary>
        /// <param name="authorizationCode">
        /// The authorization code.
        /// </param>
        /// <param name="redirectUri">
        /// The URL the user should be redrected back to 
        /// </param>
        /// <returns>
        /// Returns the <see cref="AccessToken"/>.
        /// </returns>
        public async Task<AccessToken> GetAccessTokenAsync(string authorizationCode, string redirectUri)
        {
            var data = new Dictionary<string, string>
            {
                { "client_id", this._clientId },
                { "client_secret", this._clientSecret },
                { "grant_type", "authorization_code" },
                { "code", authorizationCode },
                { "redirect_uri", redirectUri }
            };

            return await AuthorizeAsync(data);
        }

        /// <summary>
        /// Exchange this authorization code for an AccessToken, allowing requests to be mande on behalf of a user.
        /// </summary>
        /// <param name="refreshToken">
        /// The refresh token.
        /// </param>
        /// <param name="redirectUri">
        /// The URL the user should be redrected back to 
        /// </param>
        /// <returns>
        /// Returns an <see cref="AccessToken"/>.
        /// </returns>
        public async Task<AccessToken> RefreshAccessTokenAsync(string refreshToken, string redirectUri)
        {
            var data = new Dictionary<string, string>
            {
                { "client_id", this._clientId },
                { "client_secret", this._clientSecret },
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            };

            return await AuthorizeAsync(data);
        }

        /// <summary>
        /// Revoke a user's access to the Uber API via the application.
        /// </summary>
        /// <param name="accessToken">
        /// The access token being revoked.
        /// </param>
        /// <returns>
        /// Returns a boolean indicating if the Uber API returned a successful HTTP status.
        /// </returns>
        public async Task<bool> RevokeAccessTokenAsync(string accessToken)
        {
            var formData = new Dictionary<string, string>
            {
                { "client_id", this._clientId },
                { "client_secret", this._clientSecret },
                { "token", accessToken }
            };

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient
                    .PostAsync("https://login.uber.com/oauth/v2/revoke", new FormUrlEncodedContent(formData))
                    .ConfigureAwait(false);

                return response.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Authorizes a client with Uber OAuth
        /// </summary>
        /// <param name="content">
        /// The HTTP request content
        /// </param>
        /// <returns>
        /// Returns the <see cref="AccessToken"/> if authorization is successful
        /// Returns null if authorization is not successful
        /// </returns>
        private async Task<AccessToken> AuthorizeAsync(Dictionary<string, string> content)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient
                    .PostAsync("https://login.uber.com/oauth/v2/token", new FormUrlEncodedContent(content))
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    // TODO: Return error codes
                    return null;
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<AccessToken>(responseContent);
            }
        }
    }
}