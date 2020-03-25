using QuickPay.SDK.Models.Payouts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickPay.SDK.Clients
{
    public class PayoutsClient : BaseClient
    {
        public PayoutsClient(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Get payouts
        /// </summary>
        /// <returns></returns>
        public Task<List<Payout>> Index() => Get<List<Payout>>(Endpoints.Payouts());
    }
}
