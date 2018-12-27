using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Rideshare.Uber.Sdk.Extensions;
using Rideshare.Uber.Sdk.Models;

namespace Rideshare.Uber.Sdk
{
    public class BaseUberRiderService
    {
        protected HttpClient HttpClient { get; set; }

        public readonly string _version = "v1.2";

        /// <summary>
        /// Initialises a new <see cref="UberRiderServerClient"/> with the required configurations
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
        public BaseUberRiderService(AccessTokenType tokenType, string token, string baseUri)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Parameter is required", nameof(token));
            if (string.IsNullOrWhiteSpace(baseUri)) throw new ArgumentException("Parameter is required", nameof(baseUri));

            this.HttpClient = this.CreateUberHttpClient(tokenType, token, baseUri);
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

            return await this.HttpClient.UberGetAsync<ProductCollection>(url);
        }

        /// <summary>
        /// The Products Detail endpoint returns information about a specific Uber product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<UberResponse<ProductCollection>> GetProductDetailsAsync(string productId)
        {
            var url = $"/{this._version}/products/{productId}";

            return await this.HttpClient.UberGetAsync<ProductCollection>(url);
        }

        #endregion

        #region Estimates

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startLatitude"></param>
        /// <param name="startLongitude"></param>
        /// <param name="endLatitude"></param>
        /// <param name="endLongitude"></param>
        /// <param name="seatCount"></param>
        /// <returns></returns>
        public async Task<UberResponse<PriceEstimateCollection>> GetPriceEstimateAsync(float startLatitude, float startLongitude, float endLatitude, float endLongitude, int seatCount = 2)
        {
            var url = $"/{this._version}/estimates/price?start_latitude={startLatitude}&start_longitude={startLongitude}&end_latitude={endLatitude}&end_longitude={endLongitude}&seat_count={seatCount}";

            return await this.HttpClient.UberGetAsync<PriceEstimateCollection>(url);
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
        public async Task<UberResponse<TimeEstimateCollection>> GetTimeEstimateAsync(float startLatitude, float startLongitude, string productId = null)
        {
            var url = $"/{this._version}/estimates/time?start_latitude={startLatitude}&start_longitude={startLongitude}";

            if (!string.IsNullOrWhiteSpace(productId))
            {
                url += $"&product_id={productId}";
            }

            return await this.HttpClient.UberGetAsync<TimeEstimateCollection>(url);
        }

        #endregion

        #region Helper

        public HttpClient CreateUberHttpClient(AccessTokenType tokenType, string token, string baseUri)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };

            var authenticationScheme = tokenType == AccessTokenType.Server ? "Token" : "Bearer";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationScheme, token);

            // Set accept headers to JSON only
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        #endregion
    }
}