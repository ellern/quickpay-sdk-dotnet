using Newtonsoft.Json;
using QuickPay.Clients;
using QuickPay.Models.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuickPay
{
    public class QuickPayClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly string _urlInvoicing;
        private readonly string _apiKey;
        private readonly string _privateKey;
        private readonly string _userKey;


        public CardsClient Cards { get; set; }
        public FeesClient Fees { get; set; }
        public InvoicesClient Invoices { get; set; }
        public PaymentsClient Payments { get; set; }
        public SubscriptionsClient Subscriptions { get; set; }

        public QuickPayClient(string baseUrl, string baseUrlInvoicing, string apiKey, string privateKey, string userKey)
        {
            _url = baseUrl ?? "https://api.quickpay.net/";
            _urlInvoicing = baseUrlInvoicing ?? "https://invoicing.quickpay.net/";
            _apiKey = apiKey;
            _privateKey = privateKey;
            _userKey = userKey;
            _httpClient = BuildHttpClient(_url, _apiKey, null);

            Cards = new CardsClient(_httpClient);
            Fees = new FeesClient(_httpClient);
            Invoices = new InvoicesClient(BuildHttpClient(_urlInvoicing, _userKey, "application/vnd.api+json"));
            Payments = new PaymentsClient(_httpClient);
            Subscriptions = new SubscriptionsClient(_httpClient);
        }

        /// <summary>
        /// Get the QuickPay API Changelog
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

        public bool Verify(string checksum, string body) => checksum.Equals(Sign(body, _privateKey));

        private string Sign(string value, string privateKey)
        {
            var e = Encoding.UTF8;

            var hmac = new HMACSHA256(e.GetBytes(privateKey));
            byte[] b = hmac.ComputeHash(e.GetBytes(value));

            var s = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                s.Append(b[i].ToString("x2"));
            }

            return s.ToString();
        }

        private HttpClient BuildHttpClient(string baseUrl, string authKey, string accept)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{authKey}")));
            httpClient.DefaultRequestHeaders.Add("Accept-Version", "v10");
            httpClient.DefaultRequestHeaders.Add("Accept", accept ?? "application/json");
            httpClient.DefaultRequestHeaders.Add("language", "da");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "QuickPay SDK .NET Core");

            return httpClient;
        }
    }
}
