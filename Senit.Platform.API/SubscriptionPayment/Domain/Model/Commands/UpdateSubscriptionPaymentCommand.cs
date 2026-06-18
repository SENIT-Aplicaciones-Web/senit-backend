namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;

/// <summary>
///     Command used to update a subscriptionpayment.
/// </summary>
public record UpdateSubscriptionPaymentCommand(
    string Id,
    string SubscriptionId,
    string HotelId,
    string Plan,
    decimal Amount,
    string Method,
    string Status,
    DateTime? PaidAt);
