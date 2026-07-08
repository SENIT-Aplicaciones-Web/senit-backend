using Microsoft.Extensions.Options;
using Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;

namespace Senit.Platform.API.SubscriptionPayment.Infrastructure.PaymentGateway.Simulated;

/// <summary>
///     Builds local checkout URLs that simulate a Stripe Checkout subscription session.
/// </summary>
public class SimulatedStripeSubscriptionPaymentGateway(
    IOptions<SimulatedStripeOptions> options) : ISimulatedSubscriptionPaymentGateway
{
    public SimulatedCheckoutSessionResult CreateSession(
        string sessionId,
        string plan,
        decimal amount,
        string customerEmail)
    {
        return new SimulatedCheckoutSessionResult(
            sessionId,
            BuildFrontendUrl($"/checkout/simulated?session_id={Uri.EscapeDataString(sessionId)}&customer_email={Uri.EscapeDataString(customerEmail)}"),
            BuildSuccessUrl(sessionId),
            BuildFrontendUrl("/sign-in"),
            plan,
            amount,
            "PEN",
            "open",
            customerEmail);
    }

    public string BuildSuccessUrl(string sessionId)
    {
        return BuildFrontendUrl($"/checkout/success?session_id={Uri.EscapeDataString(sessionId)}");
    }

    private string BuildFrontendUrl(string path)
    {
        var baseUrl = string.IsNullOrWhiteSpace(options.Value.FrontendBaseUrl)
            ? "http://localhost:5173"
            : options.Value.FrontendBaseUrl.TrimEnd('/');

        return $"{baseUrl}{path}";
    }
}
