using System;

namespace QuickPay.SDK
{
    internal static partial class Endpoints
    {
        public static Uri Cards() => "cards".FormatUri();
        public static Uri Cards(string cardId) => $"cards/{cardId}".FormatUri();
        public static Uri Cards(string cardId, string action) => $"cards/{cardId}/{action}".FormatUri();
        public static Uri CardsAuthorize(string cardId) => $"cards/{cardId}/authorize".FormatUri();
        public static Uri CardsCancel(string cardId) => $"cards/{cardId}/cancel".FormatUri();
        public static Uri CardsLink(string cardId) => $"cards/{cardId}/link".FormatUri();
        public static Uri CardsTokens(string cardId) => $"cards/{cardId}/tokens".FormatUri();
        public static Uri Changelog() => "changelog".FormatUri();
        public static Uri Fees() => "fees".FormatUri();
        public static Uri Fees(string acquirer, string paymentMethod) => $"fees/{acquirer}/{paymentMethod}".FormatUri();
        public static Uri FeesFormulas() => "fees/formulas".FormatUri();
        public static Uri FeesFormulas(string acquirer) => $"fees/formulas/{acquirer}".FormatUri();
        public static Uri FeesFormulas(string acquirer, string paymentMethod) => $"fees/formulas/{acquirer}/{paymentMethod}".FormatUri();
        public static Uri Invoices() => "invoices".FormatUri();
        public static Uri Invoices(int invoiceId) => $"invoices/{invoiceId}".FormatUri();
        public static Uri Payments() => "payments".FormatUri();
        public static Uri Payments(int paymentId) => $"payments/{paymentId}".FormatUri();
        public static Uri Payments(int paymentId, string action) => $"payments/{paymentId}/{action}".FormatUri();
        public static Uri PaymentsAuthorize(int paymentId) => $"payments/{paymentId}/authorize".FormatUri();
        public static Uri PaymentsCapture(int paymentId) => $"payments/{paymentId}/capture".FormatUri();
        public static Uri Ping() => "ping".FormatUri();
        public static Uri Subscriptions() => "subscriptions".FormatUri();
        public static Uri Subscriptions(int subscriptionId) => $"subscriptions/{subscriptionId}".FormatUri();
        public static Uri Subscriptions(int subscriptionId, string action) => $"subscriptions/{subscriptionId}/{action}".FormatUri();
    }
}
