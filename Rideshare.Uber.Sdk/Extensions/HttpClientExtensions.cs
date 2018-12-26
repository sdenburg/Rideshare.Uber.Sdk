using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rideshare.Uber.Sdk.Models;

namespace Rideshare.Uber.Sdk.Extensions
{
    public static class HttpClientExtensions
    {
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
        public static async Task<UberResponse<T>> UberGetAsync<T>(this HttpClient httpClient, string url)
        {
            var uberResponse = new UberResponse<T>();

            var response = await httpClient
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
        public static async Task<UberResponse<T>> UberPostAsync<T>(this HttpClient httpClient, string url, HttpContent content)
        {
            var uberResponse = new UberResponse<T>();

            var response = await httpClient
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
        /// Makes a PATCH request.
        /// </summary>
        /// <typeparam name="T">
        /// The response data type.
        /// </typeparam>
        /// <param name="url">
        /// The URL being requested.
        /// </param>
        /// <param name="content">
        /// The content being PATCH-ed.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T"/>.
        /// </returns>
        public static async Task<UberResponse<T>> UberPatchAsync<T>(this HttpClient httpClient, string url, HttpContent content)
        {
            var uberResponse = new UberResponse<T>();

            var response = await httpClient
                .PatchAsync(url, content)
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
        public static async Task<UberResponse<bool>> UberDeleteAsync(this HttpClient httpClient, string url)
        {
            var uberResponse = new UberResponse<bool>();

            var response = await httpClient
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

        /// <summary>
        /// Send a PATCH request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="client"/> was null.</exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri"/> was null.</exception>
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            return client.PatchAsync(CreateUri(requestUri), content);
        }

        /// <summary>
        /// Send a PATCH request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="client"/> was null.</exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri"/> was null.</exception>
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content)
        {
            return client.PatchAsync(requestUri, content, CancellationToken.None);
        }

        /// <summary>
        /// Send a PATCH request with a cancellation token as an asynchronous operation.
        /// </summary>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="client"/> was null.</exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri"/> was null.</exception>
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return client.PatchAsync(CreateUri(requestUri), content, cancellationToken);
        }

        /// <summary>
        /// Send a PATCH request with a cancellation token as an asynchronous operation.
        /// </summary>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="client"/> was null.</exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri"/> was null.</exception>
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {
                Content = content
            };

            return client.SendAsync(request, cancellationToken);
        }

        private static Uri CreateUri(string uri)
        {
            return string.IsNullOrEmpty(uri) ? null : new Uri(uri, UriKind.RelativeOrAbsolute);
        }
    }
}
