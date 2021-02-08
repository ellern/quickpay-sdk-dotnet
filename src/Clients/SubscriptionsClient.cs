using QuickPay.SDK.Models.Payments;
using QuickPay.SDK.Models.Subscriptions;
using System.Collections.Generic;
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

        public Task<Subscription> Update(int subscriptionId, string description, Dictionary<string, string> variables)
        {
            var data = new Dictionary<string, object>
            {
                { "description", description },
                { "variables", variables },
            };

            return PatchJson<Subscription>(Endpoints.Subscriptions(subscriptionId), data);
        }

        public Task<PaymentLinkUrl> CreateOrUpdatePaymentLink(int subscriptionId, int amount, bool autoFee, string language, string paymentMethods, string continueUrl, string cancelUrl, string callbackUrl, bool framed) => CreateOrUpdatePaymentLink(subscriptionId, amount, autoFee, language, paymentMethods, continueUrl, cancelUrl, callbackUrl, framed, null);

        public Task<PaymentLinkUrl> CreateOrUpdatePaymentLink(int subscriptionId, int amount, bool autoFee, string language, string paymentMethods, string continueUrl, string cancelUrl, string callbackUrl, bool framed, string brandingId)
        {
            var data = new Dictionary<string, object>
            {
                { "amount", amount },
                { "auto_fee", autoFee },
            };

            if (language != null)
            {
                data["language"] = language;
            }

            if (paymentMethods != null)
            {
                data["payment_methods"] = paymentMethods;
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

            if (framed)
            {
                data["framed"] = true;
            }

            if (brandingId != null)
            {
                data["branding_id"] = brandingId;
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
