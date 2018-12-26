using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Parameter is required", "token");
            if (string.IsNullOrWhiteSpace(baseUri)) throw new ArgumentException("Parameter is required", "baseUri");

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };

            var authenticationScheme = tokenType == AccessTokenType.Server ? "Token" : "Bearer";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationScheme, token);

            // Set accept headers to JSON only
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
            var url = $"/{_version}/products?latitude={latitude}&longitude={longitude}";

            return await GetAsync<ProductCollection>(url);
        }

        #endregion

        #region Estimates

        public async Task<UberResponse<PriceEstimateCollection>> GetPriceEstimateAsync(float startLatitude, float startLongitude, float endLatitude, float endLongitude)
        {
            var url = $"/{_version}/estimates/price?start_latitude={startLatitude}&start_longitude={startLongitude}&end_latitude={endLatitude}&end_longitude={endLongitude}";

            return await GetAsync<PriceEstimateCollection>(url);
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
            var url = $"/{_version}/estimates/time?start_latitude={startLatitude}&start_longitude={startLongitude}";

            if (!string.IsNullOrWhiteSpace(customerId))
            {
                url += string.Format("&customer_uuid={0}", customerId);
            }

            if (!string.IsNullOrWhiteSpace(productId))
            {
                url += string.Format("&product_id={0}", productId);
            }

            return await GetAsync<TimeEstimateCollection>(url);
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
            var url = $"/{_version}/requests";

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

            return await PostAsync<Request>(url, content);
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
            var url = $"/{_version}/requests/{requestId}";

            return await GetAsync<RequestDetails>(url);
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
            var url = $"/{_version}/requests/{requestId}/map";

            return await GetAsync<RequestMap>(url);
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
            var url = $"/{_version}/requests/{requestId}";

            return await DeleteAsync(url);
        }

        #endregion

        #region Other

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
            var url = $"/{_version}/promotions?start_latitude={startLatitude}&start_longitude={startLongitude}&end_latitude={endLatitude}&end_longitude={endLongitude}";

            return await GetAsync<Promotion>(url);
        }

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
            var url = $"/_version/history?offset={offset}&limit={limit}";

            return await GetAsync<UserActivity>(url);
        }

        /// <summary>
        /// Gets the user's Uber profile.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="UserProfile"/>.
        /// </returns>
        public async Task<UberResponse<UserProfile>> GetUserProfileAsync()
        {
            var url = $"/{_version}/me";

            return await GetAsync<UserProfile>(url);
        }

        #endregion

        #region Helper

        /// <summary>
        /// Makes a GET request.
        /// </summary>
        /// <typeparam name="T">
        /// The response data type.
        /// </typeparam>
        /// <param name="url">
        /// The URL being requested.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T"/>.
        /// </returns>
        private async Task<UberResponse<T>> GetAsync<T>(string url)
        {
            var uberResponse = new UberResponse<T>();

            var response = await _httpClient
                .GetAsync(url)
                .ConfigureAwait(false);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                uberResponse.Data = JsonConvert.DeserializeObject<T>(responseContent);
            }
            else
            {
                uberResponse.Error = JsonConvert.DeserializeObject<UberError>(responseContent);
            }

            return uberResponse;
        }

        /// <summary>
        /// Makes a POST request.
        /// </summary>
        /// <typeparam name="T">
        /// The response data type.
        /// </typeparam>
        /// <param name="url">
        /// The URL being requested.
        /// </param>
        /// <param name="content">
        /// The content being POST-ed.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T"/>.
        /// </returns>
        private async Task<UberResponse<T>> PostAsync<T>(string url, HttpContent content)
        {
            var uberResponse = new UberResponse<T>();

            var response = await _httpClient
                .PostAsync(url, content)
                .ConfigureAwait(false);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                uberResponse.Data = JsonConvert.DeserializeObject<T>(responseContent);
            }
            else
            {
                uberResponse.Error = JsonConvert.DeserializeObject<UberError>(responseContent);
            }

            return uberResponse;
        }

        /// <summary>
        /// Makes a DELETE request.
        /// </summary>
        /// <param name="url">
        /// The URL being requested.
        /// </param>
        /// <returns>
        /// Returns a boolean indicating if the Uber API returned a successful HTTP status.
        /// </returns>
        private async Task<UberResponse<bool>> DeleteAsync(string url)
        {
            var uberResponse = new UberResponse<bool>();

            var response = await _httpClient
                .DeleteAsync(url)
                .ConfigureAwait(false);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                uberResponse.Data = true;
            }
            else
            {
                uberResponse.Error = JsonConvert.DeserializeObject<UberError>(responseContent);
            }

            return uberResponse;
        }

        #endregion
    }
}