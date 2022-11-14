using System.Text.Json.Serialization;
using QuickPay.SDK.Models.Fees;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickPay.SDK.Clients
{
    public class FeesClient : BaseClient
    {
        public FeesClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<CalculatedFee> GetFee(int amount, string acquirer, string paymentMethod)
        {
            if (acquirer == null || paymentMethod == null)
                return null;

            var data = new
            {
                amount = amount
            };

            var request = await PostJson(Endpoints.Fees(acquirer, paymentMethod), data).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JSON.Deserialize<CalculatedFee>(response);
        }

        public Task<List<FeeFormula>> GetFeeFormulas() => Get<List<FeeFormula>>(Endpoints.FeesFormulas());

        public Task<List<FeeFormula>> GetFeeFormulas(string acquirer) => Get<List<FeeFormula>>(Endpoints.FeesFormulas(acquirer));

        public Task<FeeFormula> GetFeeFormula(string acquirer, string paymentMethod) => Get<FeeFormula>(Endpoints.FeesFormulas(acquirer, paymentMethod));
    }
}
