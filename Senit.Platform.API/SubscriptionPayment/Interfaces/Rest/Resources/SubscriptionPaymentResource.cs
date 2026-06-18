namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a subscriptionpayment.
/// </summary>
public record SubscriptionPaymentResource(
    string Id,
    string SubscriptionId,
    string HotelId,
    string Plan,
    decimal Amount,
    string Method,
    string Status,
    DateTime? PaidAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
