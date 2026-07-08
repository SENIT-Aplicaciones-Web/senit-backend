namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;

/// <summary>
///     Command used to start a simulated Stripe checkout for a hotel subscription.
/// </summary>
public record CreateSimulatedSubscriptionCheckoutSessionCommand(
    string Username,
    string Email,
    string Password,
    string Plan);
