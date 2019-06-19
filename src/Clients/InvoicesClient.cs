using QuickPay.SDK.Models.Invoices;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickPay.SDK.Clients
{
    public class InvoicesClient : BaseClient
    {
        public InvoicesClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<Invoices> Index() => Get<Invoices>(Endpoints.Invoices());

        public Task<Invoice> Details(int invoiceId) => Get<Invoice>(Endpoints.Invoices(invoiceId));
    }
}
