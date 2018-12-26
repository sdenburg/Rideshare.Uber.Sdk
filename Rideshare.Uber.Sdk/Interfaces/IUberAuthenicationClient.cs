using System.Collections.Generic;
using System.Threading.Tasks;
using Rideshare.Uber.Sdk.Models;

namespace Rideshare.Uber.Sdk.Interfaces
{
    public interface IUberAuthenticationClient
    {
        /// <summary>
        /// Generates an Uber OAuth authorization URL based on scopes, state and redirect.
        /// </summary>
        /// <param name="scopes">
        /// The permission scope/s being requested.
        /// </param>
        /// <param name="state">
        /// State which will be passed back to you to prevent tampering.
        /// </param>
        /// <param name="redirectUri">
        /// The URI we will redirect back to after an authorization by the resource owner.
        /// </param>
        /// <returns>
        /// Returns the OAuth authorization URL.
        /// </returns>
        string GetAuthorizeUrl(List<string> scopes = null, string state = null, string redirectUri = null);

        /// <summary>
        /// Exchange this authorization code for an AccessToken, allowing requests to be mande on behalf of a user.
        /// </summary>
        /// <param name="authorizationCode">
        /// The authorization code.
        /// </param>
        /// <param name="redirectUri">
        /// The URI the user should be redrected back to 
        /// </param>
        /// <returns>
        /// Returns an <see cref="AccessToken"/>.
        /// </returns>
        Task<AccessToken> GetAccessTokenAsync(string authorizationCode, string redirectUri);

        /// <summary>
        /// Exchange this authorization code for an AccessToken, allowing requests to be mande on behalf of a user.
        /// </summary>
        /// <param name="refreshToken">
        /// The refresh token.
        /// </param>
        /// <param name="redirectUri">
        /// The URI the user should be redrected back to 
        /// </param>
        /// <returns>
        /// Returns an <see cref="AccessToken"/>.
        /// </returns>
        Task<AccessToken> RefreshAccessTokenAsync(string refreshToken, string redirectUri);

        /// <summary>
        /// Revoke a user's access to the Uber API via the application.
        /// </summary>
        /// <param name="accessToken">
        /// The access token being revoked.
        /// </param>
        /// <returns>
        /// Returns a boolean indicating if the Uber API returned a successful HTTP status.
        /// </returns>
        Task<bool> RevokeAccessTokenAsync(string accessToken);
    }
}