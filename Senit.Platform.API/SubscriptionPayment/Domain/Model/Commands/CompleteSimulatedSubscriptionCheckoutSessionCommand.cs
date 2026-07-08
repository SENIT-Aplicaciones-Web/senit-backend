namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;

/// <summary>
///     Command used to complete a simulated Stripe checkout session.
/// </summary>
public record CompleteSimulatedSubscriptionCheckoutSessionCommand(string SessionId);
