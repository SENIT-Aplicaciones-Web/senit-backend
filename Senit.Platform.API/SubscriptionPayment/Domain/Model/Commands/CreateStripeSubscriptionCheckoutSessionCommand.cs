namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;

/// <summary>
///     Command used to start a Stripe hosted Checkout session for a hotel subscription.
/// </summary>
public record CreateStripeSubscriptionCheckoutSessionCommand(
    string Username,
    string Email,
    string Password,
    string Plan);
