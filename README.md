QuickPay SDK for .NET Core
======================
[![NuGet](https://img.shields.io/nuget/dt/QuickPay.SDK.svg)](https://www.nuget.org/packages/QuickPay.SDK) [![NuGet](https://img.shields.io/nuget/vpre/QuickPay.SDK.svg)](https://www.nuget.org/packages/QuickPay.SDK)

The QuickPay SDK for .NET Core project is a client for [QuickPay API](https://learn.quickpay.net/tech-talk/api). 

Currently supports QuickPay API version: `v10`

### Installing QuickPay.SDK

You should install [QuickPay.SDK with NuGet](https://www.nuget.org/packages/QuickPay.SDK):

    Install-Package QuickPay.SDK
    
Or via the .NET Core command line interface:

    dotnet add package QuickPay.SDK

Either commands, from Package Manager Console or .NET Core CLI, will download and install QuickPay.SDK and all required dependencies.

### Using QuickPay.SDK

Initiate a new instance of the `QuickPayClient` with your API keys

    var quickPayClient = new QuickPay.SDK.QuickPayClient("api key", "private key", "user key");

#### Payments

For a single transaction you'll need to create a new payment.

	var payment = await quickpayClient.Payments.Create("DKK", "order id", new Dictionary<string, string> { "userId", "et user id" } }).ConfigureAwait(false);

Next you'll need to authorize the payment using a link. Redirect customer to the returned `link.Url` where the customer can fill in card information.

    var link = await quickPayClient.Payments.CreateOrUpdatePaymentLink(payment.Id, 100).ConfigureAwait(false);

After your customer has filled in the card information using the link, the payment is now authorized and ready to be captured.

    var capture = await quickpayClient.Payments.Capture(payment.Id, 100).ConfigureAwait(false);

Done.

#### Subscriptions

If you instead want to use subscriptions, you'll first need to create a new subscription.

    var subscription = await quickPayClient.Subscriptions.Create("order id", "DKK", "description", "descriptor", new Dictionary<string, string> { "userId", "et user id" } }).ConfigureAwait(false);

Create a link to where the customer can fill in credit card information and authorize the subscription.

    var link = await quickPayClient.Subscriptions.CreateOrUpdatePaymentLink(subscription.Id, 100, false, "da", "paymentMethods", "ContinueUrl", "CancelUrl", "CallbackUrl", true).ConfigureAwait(false);

To capture a subscription you just call the recurring method with a auto capture parameter

    var authorizeAndCapture = await quickpayClient.Subscriptions.Recurring(subscriptionId, "orderId", amount, false, true, "descriptor).ConfigureAwait(false);
