namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;

/// <summary>
///     Command used to create a subscriptionpayment.
/// </summary>
public record CreateSubscriptionPaymentCommand(
    string SubscriptionId,
    string HotelId,
    string Plan,
    decimal Amount,
    string Method,
    string Status,
    DateTime? PaidAt);
