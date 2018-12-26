using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rideshare.Uber.Sdk.Extensions;
using Rideshare.Uber.Sdk.Interfaces;
using Rideshare.Uber.Sdk.Models;

namespace Rideshare.Uber.Sdk
{
    public class UberClient : IUberClient
    {
        protected readonly HttpClient _httpClient;

        public readonly string _version = "v1.2";

        /// <summary>
        /// Initialises a new <see cref="UberClient"/> with the required configurations
        /// </summary>
        /// <param name="tokenType">
        /// The token type - server or client
        /// </param>
        /// <param name="token">
        /// The token
        /// </param>
        /// <param name="baseUri">
        /// The base URI, defaults to production - https://api.uber.com
        /// </param>
        public UberClient(AccessTokenType tokenType, string token, string baseUri = "https://api.uber.com")
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Parameter is required", nameof(token));
            if (string.IsNullOrWhiteSpace(baseUri)) throw new ArgumentException("Parameter is required", nameof(baseUri));

            this._httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };

            var authenticationScheme = tokenType == AccessTokenType.Server ? "Token" : "Bearer";
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationScheme, token);

            // Set accept headers to JSON only
            this._httpClient.DefaultRequestHeaders.Accept.Clear();
            this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Products

        /// <summary>
        /// Gets the products available at a given location.
        /// </summary>
        /// <param name="latitude">
        /// The location latitude.
        /// </param>
        /// <param name="longitude">
        /// The location longitude.
        /// </param>
        /// <returns>
        /// Returns a <see cref="ProductCollection"/>.
        /// </returns>
        public async Task<UberResponse<ProductCollection>> GetProductsAsync(float latitude, float longitude)
        {
            var url = $"/{this._version}/products?latitude={latitude}&longitude={longitude}";

            return await this._httpClient.UberGetAsync<ProductCollection>(url);
        }

        #endregion

        #region Estimates

        public async Task<UberResponse<PriceEstimateCollection>> GetPriceEstimateAsync(float startLatitude, float startLongitude, float endLatitude, float endLongitude)
        {
            var url = $"/{this._version}/estimates/price?start_latitude={startLatitude}&start_longitude={startLongitude}&end_latitude={endLatitude}&end_longitude={endLongitude}";

            return await this._httpClient.UberGetAsync<PriceEstimateCollection>(url);
        }

        /// <summary>
        /// Gets an ETA for each product available at a given location.
        /// </summary>
        /// <param name="startLatitude">
        /// The start location latitude.
        /// </param>
        /// <param name="startLongitude">
        /// The start location longitude.
        /// </param>
        /// <param name="customerId">
        /// Optional customer ID. Uber documentation describes as a "Unique customer identifier to be used for experience customization."
        /// </param>
        /// <param name="productId">
        /// Optional product ID to filter results.
        /// </param>
        /// <returns>
        /// Returns a <see cref="TimeEstimateCollection"/>.
        /// </returns>
        public async Task<UberResponse<TimeEstimateCollection>> GetTimeEstimateAsync(float startLatitude, float startLongitude, string customerId = null, string productId = null)
        {
            var url = $"/{this._version}/estimates/time?start_latitude={startLatitude}&start_longitude={startLongitude}";

            if (!string.IsNullOrWhiteSpace(customerId))
            {
                url += $"&customer_uuid={customerId}";
            }

            if (!string.IsNullOrWhiteSpace(productId))
            {
                url += $"&product_id={productId}";
            }

            return await this._httpClient.UberGetAsync<TimeEstimateCollection>(url);
        }

        #endregion

        #region Requests

        /// <summary>
        /// Makes a pickup request.
        /// </summary>
        /// <param name="productId">
        /// The product ID.
        /// </param>
        /// <param name="startLatitude">
        /// The start location latitude.
        /// </param>
        /// <param name="startLongitude">
        /// The start location longitude.
        /// </param>
        /// <param name="endLatitude">
        /// The end location latitude.
        /// </param>
        /// <param name="endLongitude">
        /// The end location longitude.
        /// </param>
        /// <param name="surgeConfirmationId">
        /// The surge pricing confirmation ID.
        /// </param>
        /// <returns>
        /// Returns a <see cref="Request"/>.
        /// </returns>
        public async Task<UberResponse<Request>> RequestAsync(string productId, float startLatitude, float startLongitude, float endLatitude, float endLongitude, string surgeConfirmationId = null)
        {
            var url = $"/{this._version}/requests";

            var postData = new Dictionary<string, string>
            {
                { "product_id", productId },
                { "start_latitude", startLatitude.ToString("0.00000") },
                { "start_longitude", startLongitude.ToString("0.00000") },
                { "end_latitude", endLatitude.ToString("0.00000") },
                { "end_longitude", endLongitude.ToString("0.00000") },
            };

            if (!string.IsNullOrWhiteSpace(surgeConfirmationId))
            {
                postData.Add("surge_confirmation_id", surgeConfirmationId);
            }

            var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

            return await this._httpClient.UberPostAsync<Request>(url, content);
        }

        /// <summary>
        /// Gets a request details.
        /// </summary>
        /// <param name="requestId">
        /// The request ID.
        /// </param>
        /// <returns>
        /// Returns a <see cref="RequestDetails"/>.
        /// </returns>
        public async Task<UberResponse<RequestDetails>> GetRequestDetailsAsync(string requestId)
        {
            var url = $"/{this._version}/requests/{requestId}";

            return await this._httpClient.UberGetAsync<RequestDetails>(url);
        }

        /// <summary>
        /// Gets the map for a given request.
        /// </summary>
        /// <param name="requestId">
        /// The request ID.
        /// </param>
        /// <returns>
        /// Returns a <see cref="RequestMap"/>.
        /// </returns>
        public async Task<UberResponse<RequestMap>> GetRequestMapAsync(string requestId)
        {
            var url = $"/{this._version}/requests/{requestId}/map";

            return await this._httpClient.UberGetAsync<RequestMap>(url);
        }

        /// <summary>
        /// Cancels a given request.
        /// </summary>
        /// <param name="requestId">
        /// The request ID.
        /// </param>
        /// <returns>
        /// Returns a boolean indicating if the Uber API returned a successful HTTP status.
        /// </returns>
        public async Task<UberResponse<bool>> CancelRequestAsync(string requestId)
        {
            var url = $"/{this._version}/requests/{requestId}";

            return await this._httpClient.UberDeleteAsync(url);
        }

        #endregion

        #region Promotions

        /// <summary>
        /// Gets a promotion available to new users based on location.
        /// </summary>
        /// <param name="startLatitude">
        /// The start location latitude.
        /// </param>
        /// <param name="startLongitude">
        /// The start location longitude.
        /// </param>
        /// <param name="endLatitude">
        /// The end location latitude.
        /// </param>
        /// <param name="endLongitude">
        /// The end location longitude.
        /// </param>
        /// <returns>
        /// Returns a <see cref="Promotion"/>.
        /// </returns>
        public async Task<UberResponse<Promotion>> GetPromotionAsync(float startLatitude, float startLongitude, float endLatitude, float endLongitude)
        {
            var url = $"/{this._version}/promotions?start_latitude={startLatitude}&start_longitude={startLongitude}&end_latitude={endLatitude}&end_longitude={endLongitude}";

            return await this._httpClient.UberGetAsync<Promotion>(url);
        }

        #endregion

        #region Riders

        /// <summary>
        /// Gets a list of the user's Uber activity.
        /// </summary>
        /// <param name="offset">
        /// The results offset.
        /// </param>
        /// <param name="limit">
        /// The results limit.
        /// </param>
        /// <returns>
        /// Returns a <see cref="UserActivity"/>.
        /// </returns>
        public async Task<UberResponse<UserActivity>> GetUserActivityAsync(int offset, int limit)
        {
            var url = $"/{this._version}/history?offset={offset}&limit={limit}";

            return await this._httpClient.UberGetAsync<UserActivity>(url);
        }

        /// <summary>
        /// The User Profile endpoint returns information about the Uber user that has authorized with the application.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="UserProfile"/>.
        /// </returns>
        public async Task<UberResponse<UserProfile>> GetUserProfileAsync()
        {
            var url = $"/{this._version}/me";

            return await this._httpClient.UberGetAsync<UserProfile>(url);
        }

        #endregion
    }
}