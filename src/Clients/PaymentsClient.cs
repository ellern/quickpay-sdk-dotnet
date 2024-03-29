﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using QuickPay.SDK.Models.Payments;

namespace QuickPay.SDK.Clients
{
    public class PaymentsClient : BaseClient
    {
        private readonly HttpClient _httpClient;

        public PaymentsClient(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<Payment>> Index() => Get<List<Payment>>(Endpoints.Payments());

        public Task<Payment> Get(int paymentId) => Get<Payment>(Endpoints.Payments(paymentId));

        public async Task<Payment> Session(int paymentId, int amount, string callbackUrl)
        {
            var data = new
            {
                amount
            };

            Dictionary<string, string> headers = null;

            if (callbackUrl != null)
            {
                headers = new Dictionary<string, string>
                {
                    { "QuickPay-Callback-Url", callbackUrl }
                };
            }

            var request = await PostJson(Endpoints.Payments(paymentId, "session"), data, headers).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JSON.Deserialize<Payment>(response);
        }

        public async Task<Payment> Authorize(int paymentId, int amount, string cardToken, bool autoCapture, string callbackUrl)
        {
            var data = new
            {
                amount,
                auto_capture = autoCapture,
                card = new
                {
                    token = cardToken
                },
                //person = new
                //{
                //    first_name = "",
                //    last_name = "",
                //    email = ""
                //}
            };

            Dictionary<string, string> headers = null;

            if (callbackUrl != null)
            {
                headers = new Dictionary<string, string>
                {
                    { "QuickPay-Callback-Url", callbackUrl }
                };
            }

            var request = await PostJson(Endpoints.PaymentsAuthorize(paymentId), data, headers).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JSON.Deserialize<Payment>(response);
        }

        public Task<Payment> Capture(int paymentId, int amount) => Capture(paymentId, amount, null);

        public async Task<Payment> Capture(int paymentId, int amount, string callbackUrl)
        {
            var data = new
            {
                amount = amount
            };

            Dictionary<string, string> headers = null;

            if (callbackUrl != null)
            {
                headers = new Dictionary<string, string>
                {
                    { "QuickPay-Callback-Url", callbackUrl }
                };
            }

            var request = await PostJson(Endpoints.PaymentsCapture(paymentId), data, headers).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JSON.Deserialize<Payment>(response);
        }

        public Task<Payment> Cancel(int paymentId) => PostEmpty<Payment>(Endpoints.Payments(paymentId, "cancel"));

        public Task<Payment> Refund(int paymentId, int amount) => Refund(paymentId, amount, null);

        public async Task<Payment> Refund(int paymentId, int amount, string callbackUrl)
        {
            var data = new
            {
                amount = amount
            };

            Dictionary<string, string> headers = null;

            if (callbackUrl != null)
            {
                headers = new Dictionary<string, string>
                {
                    { "QuickPay-Callback-Url", callbackUrl }
                };
            }

            var request = await PostJson(Endpoints.Payments(paymentId, "refund"), data, headers).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JSON.Deserialize<Payment>(response);
        }

        public Task<Payment> Renew(int paymentId) => Renew(paymentId, null);

        public async Task<Payment> Renew(int paymentId, string callbackUrl)
        {
            Dictionary<string, string> headers = null;

            if (callbackUrl != null)
            {
                headers = new Dictionary<string, string>
                {
                    { "QuickPay-Callback-Url", callbackUrl }
                };
            }

            var request = await PostJson(Endpoints.Payments(paymentId, "renew"), (object)null, headers).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JSON.Deserialize<Payment>(response);
        }

        public Task<Payment> Create(string currency, string orderId, Dictionary<string, string> variables) => Create(currency, orderId, null, variables);

        public async Task<Payment> Create(string currency, string orderId, string text_on_statement, Dictionary<string, string> variables)
        {
            if (orderId == null)
                orderId = DateTime.UtcNow.Ticks.ToString();

            if (orderId.Length < 4 || orderId.Length > 20)
                throw new ArgumentOutOfRangeException($"{nameof(orderId)} must be between 4 and 20 characters");

            var data = new Dictionary<string, object>
            {
                { "currency", currency },
                { "order_id", orderId },
                { "variables", variables },
            };

            if (text_on_statement != null)
            {
                data["text_on_statement"] = text_on_statement;
            }

            var request = await PostJson(Endpoints.Payments(), data).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JSON.Deserialize<Payment>(response);
        }

        public async Task<Payment> Update(int paymentId, Dictionary<string, string> variables)
        {
            var data = new Dictionary<string, object>
            {
                { "variables", variables },
            };

            var request = await PatchJson(Endpoints.Payments(paymentId), data).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JSON.Deserialize<Payment>(response);
        }

        public Task<PaymentLinkUrl> CreateOrUpdatePaymentLink(int paymentId, int amount) => CreateOrUpdatePaymentLink(paymentId, amount, false, false, null, null, null, null, null, false);

        public Task<PaymentLinkUrl> CreateOrUpdatePaymentLink(int paymentId, int amount, bool autoCapture, bool autoFee, string language, string paymentMethods, string continueUrl, string cancelUrl, string callbackUrl, bool framed)
        {
            var data = new Dictionary<string, object>
            {
                { "amount", amount },
                { "auto_capture", autoCapture },
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

            return PutJson<PaymentLinkUrl>(Endpoints.Payments(paymentId, "link"), data);
        }

        public Task<bool> DeletePaymentLink(int paymentId) => Delete(Endpoints.Payments(paymentId, "link"));
    }
}
