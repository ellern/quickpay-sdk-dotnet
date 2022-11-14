using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace QuickPay.SDK.Clients
{
    public class BaseClient
    {
        private readonly HttpClient _httpClient;

        public BaseClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        internal HttpClient HttpClient { get { return _httpClient; } }

        internal virtual async Task<T> GetAsync<T>(Uri endpoint, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<T>(stream);
                }

                var content = await StreamToStringAsync(stream).ConfigureAwait(false);

                throw new ApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = content
                };
            }
        }

        internal virtual async Task PostAsync(Uri endpoint, object data, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, endpoint))
            using (var httpContent = CreateHttpContent(data))
            {
                request.Content = httpContent;

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        internal virtual async Task<T> PostAsync<T>(Uri endpoint, object data, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, endpoint))
            using (var httpContent = CreateHttpContent(data))
            {
                request.Content = httpContent;

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        return DeserializeJsonFromStream<T>(stream);
                    }

                    var content = await StreamToStringAsync(stream).ConfigureAwait(false);

                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        Content = content
                    };
                }
            }
        }

        internal virtual async Task<T> Get<T>(Uri endpoint) => JSON.Deserialize<T>(await _httpClient.GetStringAsync(endpoint).ConfigureAwait(false));

        internal virtual async Task<T> PostEmpty<T>(Uri endpoint)
        {
            var request = await _httpClient.PostAsync(endpoint, new StringContent(JSON.Serialize(null), Encoding.UTF8, "application/json")).ConfigureAwait(false);
            return JSON.Deserialize<T>(await request.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        internal virtual Task<HttpResponseMessage> PutForm(Uri endpoint, IEnumerable<KeyValuePair<string, string>> data) => _httpClient.PutAsync(endpoint, new FormUrlEncodedContent(data));

        internal virtual Task<HttpResponseMessage> PostJson(Uri endpoint) => _httpClient.PostAsync(endpoint, new StringContent(JSON.Serialize(null), Encoding.UTF8, "application/json"));

        internal virtual Task<HttpResponseMessage> PostJson<T>(Uri endpoint, T data) => _httpClient.PostAsync(endpoint, new StringContent(JSON.Serialize(data), Encoding.UTF8, "application/json"));

        internal virtual Task<HttpResponseMessage> PostJson<T>(Uri endpoint, T data, Dictionary<string, string> headers)
        {
            var stringContent = new StringContent(JSON.Serialize(data), Encoding.UTF8, "application/json");

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    stringContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            return _httpClient.PostAsync(endpoint, stringContent);
        }

#if NETSTANDARD2_1_OR_GREATER
        internal virtual Task<HttpResponseMessage> PatchJson(Uri endpoint, object data) => _httpClient.PatchAsync(endpoint, new StringContent(JSON.Serialize(data), Encoding.UTF8, "application/json"));
#else
        internal virtual Task<HttpResponseMessage> PatchJson(Uri endpoint, object data) => _httpClient.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), endpoint) { Content = new StringContent(JSON.Serialize(data), Encoding.UTF8, "application/json") });
#endif

        internal virtual async Task<T> PostJson<T>(Uri endpoint, Dictionary<string, object> data)
        {
            var request = await _httpClient.PostAsync(endpoint, new StringContent(JSON.Serialize(data), Encoding.UTF8, "application/json")).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JSON.Deserialize<T>(response);
        }

        internal virtual async Task<T> PostJson<T>(Uri endpoint, Dictionary<string, object> data, Dictionary<string, string> headers)
        {
            var stringContent = new StringContent(JSON.Serialize(data), Encoding.UTF8, "application/json");

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    stringContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            var request = await _httpClient.PostAsync(endpoint, stringContent).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JSON.Deserialize<T>(response);
        }

        internal virtual async Task<T> PatchJson<T>(Uri endpoint, Dictionary<string, object> data)
        {
#if NETSTANDARD2_1_OR_GREATER
            var request = await _httpClient.PatchAsync(endpoint, new StringContent(JSON.Serialize(data), Encoding.UTF8, "application/json")).ConfigureAwait(false);
#else
            var request = await _httpClient.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), endpoint) { Content = new StringContent(JSON.Serialize(data), Encoding.UTF8, "application/json") }).ConfigureAwait(false);
#endif

            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JSON.Deserialize<T>(response);
        }

        internal virtual async Task<T> PutJson<T>(Uri endpoint, Dictionary<string, object> data)
        {
            var request = await _httpClient.PutAsync(endpoint, new StringContent(JSON.Serialize(data), Encoding.UTF8, "application/json")).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JSON.Deserialize<T>(response);
        }

        internal virtual async Task<bool> Delete(Uri endpoint) => (await _httpClient.DeleteAsync(endpoint).ConfigureAwait(false)).IsSuccessStatusCode;

        HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        void SerializeJsonIntoStream(object value, Stream stream)
        {
            JSON.Serialize(stream, value);
        }

        T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            return JSON.Deserialize<T>(stream);
        }

        async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
            {
                using (var sr = new StreamReader(stream))
                {
                    content = await sr.ReadToEndAsync().ConfigureAwait(false);
                }
            }

            return content;
        }

        public async Task<HttpResponseMessage> ValidateRequest(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return httpResponseMessage;
            }

            var content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var errors = JSON.Deserialize<ValidationErrors>(content);

            var sb = new StringBuilder();
            sb.AppendLine(errors.Message);
            sb.AppendLine("");

            if (errors.Errors.Count > 0)
            {
                sb.AppendLine("Errors:");
                foreach (var item in errors.Errors)
                {
                    sb.AppendLine($"  {item.Key}:");

                    foreach (var error in item.Value)
                    {
                        sb.AppendLine($"   - {error}");
                    }
                }
            }

            throw new Exception(sb.ToString());
        }
    }

    public class ValidationErrors
    {
        public string Message { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
        [JsonPropertyName("error_code")]
        public object ErrorCode { get; set; }
    }

    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }
    }

}
