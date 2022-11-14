using System.Text.Json.Serialization;
using QuickPay.SDK.Models.Payments;
using QuickPay.SDK.Models.Shared;
using QuickPay.SDK.Models.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickPay.SDK.Clients
{
    public class SubscriptionsClient : BaseClient
    {
        private readonly HttpClient _httpClient;

        public SubscriptionsClient(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<Subscription>> Index() => Get<List<Subscription>>(Endpoints.Subscriptions());

        public Task<List<Subscription>> Index(string orderId) => Get<List<Subscription>>(Endpoints.Subscriptions().ExtendQuery(new Dictionary<string, string> { { "order_id", orderId } }));

        public Task<List<Subscription>> Index(int subscriptionId) => Get<List<Subscription>>(Endpoints.Subscriptions().ExtendQuery(new Dictionary<string, string> { { "id", subscriptionId.ToString() } }));

        public Task<Subscription> Get(int subscriptionId) => Get<Subscription>(Endpoints.Subscriptions(subscriptionId));

        /// <summary>
        /// Creates a new subscription
        /// </summary>
        /// <param name="orderId">Unique order number</param>
        /// <param name="currency">Currency</param>
        /// <param name="description">Subscription description</param>
        /// <returns></returns>
        public Task<Subscription> Create(string orderId, string currency, string description) => Create(orderId, currency, description, null, null);

        /// <summary>
        /// Creates a new subscription
        /// </summary>
        /// <param name="orderId">Unique order number</param>
        /// <param name="currency">Currency</param>
        /// <param name="description">Subscription description</param>
        /// <param name="descriptor">Text to be displayed on cardholder’s statement. Max 22 ASCII chars. Currently supported by Clearhaus only.</param>
        /// <param name="variables">Custom variables</param>
        /// <returns></returns>
        public Task<Subscription> Create(string orderId, string currency, string description, string descriptor, Dictionary<string, string> variables)
        {
            var data = new Dictionary<string, object>
            {
                { "order_id", orderId },
                { "currency", currency },
                { "description", description },
                { "variables", variables },
            };

            if (descriptor != null)
                data["text_on_statement"] = descriptor;

            return PostJson<Subscription>(Endpoints.Subscriptions(), data);
        }

        public Task<Subscription> Create(string orderId, OptionalAddress shippingAddress, OptionalAddress invoiceAddress, Dictionary<string, string> variables, string currency, string description, int? brandingId, List<int> groupIds, string text_on_statement, string shopsystemName, string shopsystemVersion)
        {
            var data = new Dictionary<string, object>
            {
                { "order_id", orderId },
                { "variables", variables },
                { "currency", currency },
            };

            if (shippingAddress != null)
            {
                var shippingAddressData = new Dictionary<string, string>
                {
                    { "name", shippingAddress.Name },
                    { "att", shippingAddress.Att },
                    { "company_name", shippingAddress.CompanyName },
                    { "street", shippingAddress.Street },
                    { "house_number", shippingAddress.HouseNumber },
                    { "house_extension", shippingAddress.HouseExtension },
                    { "city", shippingAddress.City },
                    { "zip_code", shippingAddress.ZipCode },
                    { "region", shippingAddress.Region },
                    { "vat_no", shippingAddress.VatNo },
                    { "phone_number", shippingAddress.PhoneNumber },
                    { "mobile_number", shippingAddress.MobileNumber },
                    { "email", shippingAddress.Email },
                };

                if (shippingAddress.CountryCode != null)
                {
                    shippingAddressData.Add("country_code", shippingAddress.CountryCode);
                }

                data["shipping_address"] = shippingAddressData;

            }

            if (invoiceAddress != null)
            {
                var invoiceAddressData = new Dictionary<string, string>
                {
                    { "name", invoiceAddress.Name },
                    { "att", invoiceAddress.Att },
                    { "company_name", invoiceAddress.CompanyName },
                    { "street", invoiceAddress.Street },
                    { "house_number", invoiceAddress.HouseNumber },
                    { "house_extension", invoiceAddress.HouseExtension },
                    { "city", invoiceAddress.City },
                    { "zip_code", invoiceAddress.ZipCode },
                    { "region", invoiceAddress.Region },
                    { "vat_no", invoiceAddress.VatNo },
                    { "phone_number", invoiceAddress.PhoneNumber },
                    { "mobile_number", invoiceAddress.MobileNumber },
                    { "email", invoiceAddress.Email },
                };

                if (invoiceAddress.CountryCode != null)
                {
                    invoiceAddressData.Add("country_code", invoiceAddress.CountryCode);
                }

                data["invoice_address"] = invoiceAddressData;
            }

            if (description != null)
            {
                data["description"] = description;
            }

            if (brandingId != null)
            {
                data["branding_id"] = brandingId.Value;
            }

            if (text_on_statement != null)
            {
                data["text_on_statement"] = text_on_statement;
            }

            if (shopsystemName != null || shopsystemVersion != null)
            {
                data["shopsystem"] = new Dictionary<string, string>
                {
                    { "name", shopsystemName },
                    { "version", shopsystemVersion },
                };
            }

            return PostJson<Subscription>(Endpoints.Subscriptions(), data);
        }


        /// <summary>
        /// Update existing subscription
        /// </summary>
        /// <param name="subscriptionId">Subscription ID</param>
        /// <param name="description">Subscription description</param>
        /// <param name="variables">Custom variables</param>
        /// <returns></returns>
        public Task<Subscription> Update(int subscriptionId, string description, Dictionary<string, string> variables)
        {
            var data = new Dictionary<string, object>
            {
                { "description", description },
                { "variables", variables },
            };

            return PatchJson<Subscription>(Endpoints.Subscriptions(subscriptionId), data);
        }

        public Task<PaymentLinkUrl> CreateOrUpdatePaymentLink(int subscriptionId, int amount) => CreateOrUpdatePaymentLink(subscriptionId, amount, null, null, null, null, null, null,false, null, null, null, null, null, false, null, null, false, false);

        public Task<PaymentLinkUrl> CreateOrUpdatePaymentLink(int subscriptionId, int amount, bool autoFee, string language, string paymentMethods, string continueUrl, string cancelUrl, string callbackUrl, bool framed) => CreateOrUpdatePaymentLink(subscriptionId, amount, null, language, continueUrl, cancelUrl, callbackUrl, paymentMethods, autoFee, null, null, null, null, null, framed, null, null, false, false);

        public Task<PaymentLinkUrl> CreateOrUpdatePaymentLink(int subscriptionId, int amount, bool autoFee, string language, string paymentMethods, string continueUrl, string cancelUrl, string callbackUrl, bool framed, string brandingId) => CreateOrUpdatePaymentLink(subscriptionId, amount, null, language, continueUrl, cancelUrl, callbackUrl, paymentMethods, autoFee, int.Parse(brandingId), null, null, null, null, framed, null, null, false, false);

        public Task<PaymentLinkUrl> CreateOrUpdatePaymentLink(int subscriptionId, int amount, int? agreementId, string language, string continueUrl, string cancelUrl, string callbackUrl, string paymentMethods, bool autoFee, int? brandingId, string googleAnalyticsTrackingId, string googleAnalyticsClientId, string acquirer, int? deadline, bool framed, Dictionary<string, string> brandingConfig, string customerEmail, bool invoiceAddressSelection, bool shippingAddressSelection)
        {
            var data = new Dictionary<string, object>
            {
                { "amount", amount },
                { "auto_fee", autoFee },
                { "framed", framed },
                { "invoice_address_selection", invoiceAddressSelection },
                { "shipping_address_selection", shippingAddressSelection },
            };

            if (agreementId != null)
            {
                data["agreement_id"] = agreementId.Value;
            }

            if (language != null)
            {
                data["language"] = language;
            }

            if (continueUrl != null)
            {
                data["continueurl"] = continueUrl;
            }

            if (cancelUrl != null)
            {
                data["cancelurl"] = cancelUrl;
            }

            if (callbackUrl != null)
            {
                data["callbackurl"] = callbackUrl;
            }

            if (paymentMethods != null)
            {
                data["payment_methods"] = paymentMethods;
            }

            if (brandingId != null)
            {
                data["branding_id"] = brandingId.Value;
            }

            if (googleAnalyticsTrackingId != null)
            {
                data["google_analytics_tracking_id"] = googleAnalyticsTrackingId;
            }

            if (googleAnalyticsClientId != null)
            {
                data["google_analytics_client_id"] = googleAnalyticsClientId;
            }

            if (acquirer != null)
            {
                data["acquirer"] = acquirer;
            }

            if (deadline != null)
            {
                data["deadline"] = deadline.Value;
            }

            if (brandingConfig != null)
            {
                data["branding_config"] = brandingConfig;
            }

            if (customerEmail != null)
            {
                data["customer_email"] = customerEmail;
            }

            return PutJson<PaymentLinkUrl>(Endpoints.Subscriptions(subscriptionId, "link"), data);
        }

        public Task<bool> Delete(int subscriptionId) => Delete(Endpoints.Subscriptions(subscriptionId));

        public Task<Subscription> Authorize(int subscriptionId, int amount)
        {
            var data = new Dictionary<string, object>
            {
                { "amount", amount },
            };

            return PostJson<Subscription>(Endpoints.Subscriptions(subscriptionId, "authorize"), data);
        }

        public Task<Subscription> Authorize(int subscriptionId, int amount, string cardToken)
        {
            var data = new Dictionary<string, object>
            {
                { "amount", amount },
                { "card", new Dictionary<string, object>
                    {
                        { "token", cardToken }
                    }
                }
            };

            return PostJson<Subscription>(Endpoints.Subscriptions(subscriptionId, "authorize"), data);
        }

        public Task<Subscription> Cancel(int subscriptionId) => PostEmpty<Subscription>(Endpoints.Subscriptions(subscriptionId, "cancel"));

        [Obsolete("This method uses a 'synchronized' query, which is marked for removal in QuickPay API v11")]
        public Task<Subscription> Recurring(int subscriptionId, string orderId, int amount, bool autoFee, bool autoCapture, string descriptor)
        {
            var data = new Dictionary<string, object>
            {
                { "amount", amount },
                { "auto_capture", autoCapture },
                { "auto_fee", autoFee },
                { "order_id", orderId },
            };

            if (descriptor != null)
            {
                data["text_on_statement"] = descriptor;
            }

            // HACK: Synchronized is being removed for v11

            return PostJson<Subscription>(Endpoints.Subscriptions(subscriptionId, "recurring").ExtendQuery(new Dictionary<string, string> { { "synchronized", null } }), data);
        }

        /// <summary>
        /// Create subscription recurring payment
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="orderId"></param>
        /// <param name="amount"></param>
        /// <param name="autoFee"></param>
        /// <param name="autoCapture"></param>
        /// <param name="descriptor"></param>
        /// <param name="dueDate">Format: YYYY-MM-DD</param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<Subscription> Recurring(int subscriptionId, string orderId, int amount, bool autoFee, bool autoCapture, string descriptor, string dueDate, string description, string callbackUrl)
        {
            var data = new Dictionary<string, object>
            {
                { "amount", amount },
                { "auto_capture", autoCapture },
                { "auto_fee", autoFee },
                { "order_id", orderId },
            };

            if (descriptor != null)
            {
                data["text_on_statement"] = descriptor;
            }

            if (dueDate != null)
            {
                data["due_date"] = dueDate;
            }

            if (description != null)
            {
                data["description"] = description;
            }

            Dictionary<string, string> headers = null;

            if (callbackUrl != null)
            {
                headers = new Dictionary<string, string>
                {
                    { "QuickPay-Callback-Url", callbackUrl }
                };
            }

            var httpResponseMessage = await PostJson(Endpoints.Subscriptions(subscriptionId, "recurring"), data, headers).ConfigureAwait(false);
            var response = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(response))
            {
                // HACK: MobilePay is not returning any content, instead there is a location header where we can parse the paymentId

                if (httpResponseMessage.Headers.TryGetValues("location", out var locations))
                {
                    var location = locations?.FirstOrDefault();
                    var paymentIndex = location?.IndexOf("payments/") ?? -1;
                    var operationIndex = location?.IndexOf("/operations") ?? -1;

                    if (paymentIndex > -1 && operationIndex > -1 && int.TryParse(location.Substring(paymentIndex + 9, operationIndex - (paymentIndex + 9)), out var paymentId))
                    {
                        return new Subscription
                        {
                            Id = paymentId,
                        };
                    }
                }

                return null;
            }
            else
            {
                return JSON.Deserialize<Subscription>(response);
            }
        }

        public Task<Subscription> Session(int subscriptionId, bool autoFee)
        {
            var data = new Dictionary<string, object>
            {
                { "auto_fee", autoFee },
            };

            return PostJson<Subscription>(Endpoints.Subscriptions(subscriptionId, "session"), data);
        }

        public Task<List<Payment>> GetPayments(int subscriptionId) => Get<List<Payment>>(Endpoints.Subscriptions(subscriptionId, "payments"));
    }
}
