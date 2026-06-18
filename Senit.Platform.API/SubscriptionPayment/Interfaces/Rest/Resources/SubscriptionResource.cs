namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a subscription.
/// </summary>
public record SubscriptionResource(
    string Id,
    string HotelId,
    string Plan,
    string Status,
    decimal MonthlyAmount,
    DateTime StartedAt,
    DateTime? EndsAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
