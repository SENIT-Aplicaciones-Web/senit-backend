namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;

/// <summary>
///     Command used to update a subscription.
/// </summary>
public record UpdateSubscriptionCommand(
    string Id,
    string HotelId,
    string Plan,
    string Status,
    decimal MonthlyAmount,
    DateTime StartedAt,
    DateTime? EndsAt);
