using Newtonsoft.Json;
using QuickPay.SDK.Clients;
using QuickPay.SDK.Models.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QuickPay.SDK
{
    public class QuickPayClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly string _urlInvoicing;
        private readonly string _apiKey;
        private readonly string _privateKey;
        private readonly string _userKey;

        public CallbacksClient Callbacks { get; set; }
        public CardsClient Cards { get; set; }
        public FeesClient Fees { get; set; }
        public InvoicesClient Invoices { get; set; }
        public PaymentsClient Payments { get; set; }
        public PayoutsClient Payouts { get; set; }
        public SubscriptionsClient Subscriptions { get; set; }

        /// <summary>
        /// Creates a new instance of the QuickPayClient
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="privateKey"></param>
        /// <param name="userKey"></param>
        public QuickPayClient(string apiKey, string privateKey, string userKey)
            : this(null, null, apiKey, privateKey, userKey)
        {

        }

        /// <summary>
        /// Creates a new instance of the QuickPayClient
        /// </summary>
        /// <param name="baseUrl">URL for API, if null it defaults to QuickPays standard URL</param>
        /// <param name="baseUrlInvoicing">URL for Invoicing API, if null it defaults to QuickPays standard URL</param>
        /// <param name="apiKey"></param>
        /// <param name="privateKey"></param>
        /// <param name="userKey"></param>
        public QuickPayClient(string baseUrl, string baseUrlInvoicing, string apiKey, string privateKey, string userKey)
        {
            _url = baseUrl ?? "https://api.quickpay.net/";
            _urlInvoicing = baseUrlInvoicing ?? "https://invoicing.quickpay.net/";
            _apiKey = apiKey;
            _privateKey = privateKey;
            _userKey = userKey;
            _httpClient = BuildHttpClient(_url, _apiKey, null);

            Callbacks = new CallbacksClient(_privateKey);
            Cards = new CardsClient(_httpClient);
            Fees = new FeesClient(_httpClient);
            Invoices = new InvoicesClient(BuildHttpClient(_urlInvoicing, _userKey, "application/vnd.api+json"));
            Payments = new PaymentsClient(_httpClient);
            Payouts = new PayoutsClient(_httpClient);
            Subscriptions = new SubscriptionsClient(_httpClient);
        }

        /// <summary>
        /// Get the API Changelog
        /// </summary>
        /// <returns></returns>
        public async Task<string> Changelog()
        {
            var request = await _httpClient.GetAsync(Endpoints.Changelog()).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            return response;
        }

        /// <summary>
        /// Use this to test connectivity to the API
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<Pong> Ping(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var form = new List<KeyValuePair<string, string>>();

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    form.Add(item);
                }
            }

            var request = await _httpClient.PostAsync(Endpoints.Ping(), new FormUrlEncodedContent(form)).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Pong>(response);
        }

        /// <summary>
        /// Verifies the body with the checksum and private key
        /// </summary>
        /// <param name="headerChecksum"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        [Obsolete("Use QuickPayClient.Callbacks.Verify() instead")]
        public bool Verify(string headerChecksum, string requestBody) => Callbacks.Verify(headerChecksum, requestBody);

        private HttpClient BuildHttpClient(string baseUrl, string apiKey, string accept)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{apiKey}")));
            }

            httpClient.DefaultRequestHeaders.Add("Accept-Version", "v10");
            httpClient.DefaultRequestHeaders.Add("Accept", accept ?? "application/json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "QuickPay SDK .NET Core");

            return httpClient;
        }
    }
}
