using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickPay.Models.Cards;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickPay.Clients
{
    public class CardsClient : BaseClient
    {
        public CardsClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<List<Card>> Index() => Get<List<Card>>(Endpoints.Cards());

        public Task<Card> Get(string cardId) => Get<Card>(Endpoints.Cards(cardId));

        public Task<Card> Create() => PostEmpty<Card>(Endpoints.Cards());

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

        public Task<Card> Cancel(string cardId) => PostEmpty<Card>(Endpoints.CardsCancel(cardId));

        public Task<bool> Delete(string cardId) => Delete(Endpoints.Cards(cardId));

        /// <summary>
        /// Creates a new card token
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns>CardToken</returns>
        public Task<CardToken> CreateToken(string cardId) => PostEmpty<CardToken>(Endpoints.CardsTokens(cardId));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="continueUrl"></param>
        /// <param name="cancelUrl"></param>
        /// <param name="callbackUrl"></param>
        /// <param name="framed"></param>
        /// <returns></returns>
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

            form.Add(new KeyValuePair<string, string>("language", "da"));

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
