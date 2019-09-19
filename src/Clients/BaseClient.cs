using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        internal virtual async Task<T> Get<T>(Uri endpoint) => JsonConvert.DeserializeObject<T>(await _httpClient.GetStringAsync(endpoint).ConfigureAwait(false));

        internal virtual async Task<T> PostEmpty<T>(Uri endpoint)
        {
            var request = await _httpClient.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json")).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(await request.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        internal virtual Task<HttpResponseMessage> PutForm(Uri endpoint, IEnumerable<KeyValuePair<string, string>> data) => _httpClient.PutAsync(endpoint, new FormUrlEncodedContent(data));

        internal virtual Task<HttpResponseMessage> PostJson(Uri endpoint) => _httpClient.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json"));

        internal virtual Task<HttpResponseMessage> PostJson<T>(Uri endpoint, T data) => _httpClient.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));

        //internal virtual Task<HttpResponseMessage> PatchJson(Uri endpoint, object data) => _httpClient.PatchAsync(endpoint, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")); // Patch extension only available in netstandard2.1
        internal virtual Task<HttpResponseMessage> PatchJson(Uri endpoint, object data) => _httpClient.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), endpoint) { Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json") });

        internal virtual async Task<T> PostJson<T>(Uri endpoint, Dictionary<string, object> data)
        {
            var request = await _httpClient.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(response);
        }

        internal virtual async Task<T> PatchJson<T>(Uri endpoint, Dictionary<string, object> data)
        {
            //var request = await _httpClient.PatchAsync(endpoint, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")).ConfigureAwait(false); // Patch extension only available in netstandard2.1
            var request = await _httpClient.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), endpoint) { Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json") }).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(response);
        }

        internal virtual async Task<T> PutJson<T>(Uri endpoint, Dictionary<string, object> data)
        {
            var request = await _httpClient.PutAsync(endpoint, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(response);
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
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

        T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
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
            var errors = JsonConvert.DeserializeObject<ValidationErrors>(content);

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
        [JsonProperty(PropertyName = "error_code")]
        public object ErrorCode { get; set; }
    }

    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }
    }

}
