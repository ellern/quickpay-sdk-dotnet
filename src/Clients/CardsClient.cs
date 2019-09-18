using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickPay.SDK.Models.Cards;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickPay.SDK.Clients
{
    public class CardsClient : BaseClient
    {
        public CardsClient(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Get saved cards
        /// </summary>
        /// <returns></returns>
        public Task<List<Card>> Index() => Get<List<Card>>(Endpoints.Cards());

        /// <summary>
        /// Get saved card
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public Task<Card> Get(string cardId) => Get<Card>(Endpoints.Cards(cardId));

        /// <summary>
        /// Create saved card
        /// </summary>
        /// <returns></returns>
        [Obsolete("PSD2 does not allow captures without SCA, so this basically makes saved cards useless. Use subscriptions instead.")]
        public Task<Card> Create() => PostEmpty<Card>(Endpoints.Cards());

        /// <summary>
        /// Create saved card
        /// </summary>
        /// <param name="variables">Custom variables</param>
        /// <returns></returns>
        [Obsolete("PSD2 does not allow captures without SCA, so this basically makes saved cards useless. Use subscriptions instead.")]
        public async Task<Card> Create(Dictionary<string, string> variables)
        {
            if (variables == null)
            {
                return await Create().ConfigureAwait(false);
            }
            else
            {
                var data = new
                {
                    variables = variables,
                };

                var request = await PostJson(Endpoints.Cards(), data).ConfigureAwait(false);
                var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Card>(response);
            }
        }

        /// <summary>
        /// Cancel saved card
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public Task<Card> Cancel(string cardId) => PostEmpty<Card>(Endpoints.CardsCancel(cardId));

        /// <summary>
        /// Creates a new card token
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns>CardToken</returns>
        [Obsolete("PSD2 does not allow captures without SCA, so this basically makes saved cards useless. Use subscriptions instead.")]
        public Task<CardToken> CreateToken(string cardId) => PostEmpty<CardToken>(Endpoints.CardsTokens(cardId));

        /// <summary>
        /// Create or update a card link
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        [Obsolete("PSD2 does not allow captures without SCA, so this basically makes saved cards useless. Use subscriptions instead.")]
        public Task<CardLinkUrl> CreateLink(string cardId) => CreateLink(cardId, null, null, null, false, null, null, null, null, null, null);

        /// <summary>
        /// Create or update a card link
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="continueUrl">Url that cardholder is redirected to after authorize</param>
        /// <param name="cancelUrl">Url that cardholder is redirected to after cancelation</param>
        /// <param name="callbackUrl">Endpoint for async callback</param>
        /// <param name="framed">Allow opening in iframe</param>
        /// <returns></returns>
        [Obsolete("PSD2 does not allow captures without SCA, so this basically makes saved cards useless. Use subscriptions instead.")]
        public async Task<CardLinkUrl> CreateLink(string cardId, string continueUrl, string cancelUrl, string callbackUrl, bool framed)
        {
            var form = new List<KeyValuePair<string, string>>();

            if (continueUrl != null)
            {
                form.Add(new KeyValuePair<string, string>("continueurl", continueUrl));
            }

            if (cancelUrl != null)
            {
                form.Add(new KeyValuePair<string, string>("cancelurl", cancelUrl));
            }

            if (callbackUrl != null)
            {
                form.Add(new KeyValuePair<string, string>("callbackurl", callbackUrl));
            }

            if (framed)
            {
                form.Add(new KeyValuePair<string, string>("framed", "true"));
            }

            var request = await PutForm(Endpoints.CardsLink(cardId), form).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CardLinkUrl>(response);
        }

        /// <summary>
        /// Create or update a card link
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="continueUrl">Url that cardholder is redirected to after authorize</param>
        /// <param name="cancelUrl">Url that cardholder is redirected to after cancelation</param>
        /// <param name="callbackUrl">Endpoint for async callback</param>
        /// <param name="framed">Allow opening in iframe</param>
        /// <param name="agreementId">Agreement to use. Defaults to the Payment Window agreement</param>
        /// <param name="language">Language to use. Defaults to "en"</param>
        /// <param name="acquirer">Force usage of the given acquirer</param>
        /// <param name="paymentMethods">Limit payment methods</param>
        /// <param name="brandingId">Override branding. Default is merchant default branding</param>
        /// <param name="brandingConfig">Config for branding. Will be merged with the default config in the branding</param>
        /// <returns></returns>
        [Obsolete("PSD2 does not allow captures without SCA, so this basically makes saved cards useless. Use subscriptions instead.")]
        public async Task<CardLinkUrl> CreateLink(string cardId, string continueUrl, string cancelUrl, string callbackUrl, bool framed, int? agreementId, string language, string acquirer, string paymentMethods, int? brandingId, Dictionary<string, string> brandingConfig)
        {
            var form = new List<KeyValuePair<string, string>>();

            if (continueUrl != null)
            {
                form.Add(new KeyValuePair<string, string>("continueurl", continueUrl));
            }

            if (cancelUrl != null)
            {
                form.Add(new KeyValuePair<string, string>("cancelurl", cancelUrl));
            }

            if (callbackUrl != null)
            {
                form.Add(new KeyValuePair<string, string>("callbackurl", callbackUrl));
            }

            if (framed)
            {
                form.Add(new KeyValuePair<string, string>("framed", "true"));
            }

            if (agreementId != null)
            {
                form.Add(new KeyValuePair<string, string>("agreement_id", agreementId.ToString()));
            }

            if (language != null)
            {
                form.Add(new KeyValuePair<string, string>("language", language));
            }

            if (acquirer != null)
            {
                form.Add(new KeyValuePair<string, string>("acquirer", acquirer));
            }

            if (paymentMethods != null)
            {
                form.Add(new KeyValuePair<string, string>("payment_methods", paymentMethods));
            }

            if (brandingId != null)
            {
                form.Add(new KeyValuePair<string, string>("branding_id", brandingId.ToString()));
            }

            if (brandingConfig != null)
            {
                throw new NotImplementedException("This feature is not implemented");
                //form.Add(new KeyValuePair<string, string>("branding_config", ));
            }

            //google_analytics_tracking_id Send events to Google Analytics form    string      false
            //google_analytics_client_id Send events to Google Analytics form    string      false

            var request = await PutForm(Endpoints.CardsLink(cardId), form).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CardLinkUrl>(response);
        }

        /// <summary>
        /// Deletes a saved card link
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public Task<bool> DeleteLink(string cardId) => Delete(Endpoints.CardsLink(cardId));
    }
}
