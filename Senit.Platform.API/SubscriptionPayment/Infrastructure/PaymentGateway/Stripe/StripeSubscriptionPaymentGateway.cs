using Microsoft.Extensions.Options;
using Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;
using Stripe;
using Stripe.Checkout;

namespace Senit.Platform.API.SubscriptionPayment.Infrastructure.PaymentGateway.Stripe;

/// <summary>
///     Creates and retrieves Stripe hosted Checkout subscription sessions.
/// </summary>
public class StripeSubscriptionPaymentGateway(
    IOptions<StripeCheckoutOptions> options) : IStripeSubscriptionPaymentGateway
{
    public async Task<StripeCheckoutSessionResult?> CreateSubscriptionCheckoutSessionAsync(
        string plan,
        decimal amount,
        string customerEmail,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(options.Value.SecretKey)) return null;

        var service = new SessionService();
        var sessionOptions = new SessionCreateOptions
        {
            Mode = "subscription",
            CustomerEmail = customerEmail,
            SuccessUrl = BuildFrontendUrl("/checkout/success?session_id={CHECKOUT_SESSION_ID}"),
            CancelUrl = BuildFrontendUrl("/sign-in"),
            LineItems =
            [
                new SessionLineItemOptions
                {
                    Quantity = 1,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = GetCurrency(),
                        UnitAmount = ToMinorAmount(amount),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Senit {plan} plan"
                        },
                        Recurring = new SessionLineItemPriceDataRecurringOptions
                        {
                            Interval = "month"
                        }
                    }
                }
            ],
            Metadata = new Dictionary<string, string>
            {
                ["senit_plan"] = plan,
                ["senit_checkout_type"] = "hotel_subscription"
            }
        };

        var requestOptions = new RequestOptions { ApiKey = options.Value.SecretKey };
        var session = await service.CreateAsync(sessionOptions, requestOptions, cancellationToken);
        return ToResult(session, plan, amount, customerEmail);
    }

    public async Task<StripeCheckoutSessionResult?> RetrieveCheckoutSessionAsync(
        string sessionId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(options.Value.SecretKey)) return null;

        var service = new SessionService();
        var requestOptions = new RequestOptions { ApiKey = options.Value.SecretKey };
        var session = await service.GetAsync(sessionId, null, requestOptions, cancellationToken);
        var plan = session.Metadata != null && session.Metadata.TryGetValue("senit_plan", out var metadataPlan)
            ? metadataPlan
            : "Basic";
        var amount = session.AmountTotal.HasValue ? session.AmountTotal.Value / 100m : 0m;
        var customerEmail = session.CustomerEmail ?? string.Empty;

        return ToResult(session, plan, amount, customerEmail);
    }

    private StripeCheckoutSessionResult ToResult(Session session, string plan, decimal amount, string customerEmail)
    {
        var status = session.Status == "complete" ? "completed" : session.Status ?? "open";
        return new StripeCheckoutSessionResult(
            session.Id,
            session.Url ?? string.Empty,
            BuildFrontendUrl($"/checkout/success?session_id={Uri.EscapeDataString(session.Id)}"),
            BuildFrontendUrl("/sign-in"),
            plan,
            amount,
            GetCurrency().ToUpperInvariant(),
            status,
            session.PaymentStatus ?? "unpaid",
            customerEmail);
    }

    private string GetCurrency()
    {
        return string.IsNullOrWhiteSpace(options.Value.Currency)
            ? "pen"
            : options.Value.Currency.Trim().ToLowerInvariant();
    }

    private string BuildFrontendUrl(string path)
    {
        var baseUrl = string.IsNullOrWhiteSpace(options.Value.FrontendBaseUrl)
            ? "http://localhost:5173"
            : options.Value.FrontendBaseUrl.TrimEnd('/');

        return $"{baseUrl}{path}";
    }

    private static long ToMinorAmount(decimal amount)
    {
        return Convert.ToInt64(decimal.Round(amount * 100m, 0, MidpointRounding.AwayFromZero));
    }
}
