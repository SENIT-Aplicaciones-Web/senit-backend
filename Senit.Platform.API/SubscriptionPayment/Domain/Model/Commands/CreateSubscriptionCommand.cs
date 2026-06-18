namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;

/// <summary>
///     Command used to create a subscription.
/// </summary>
public record CreateSubscriptionCommand(
    string HotelId,
    string Plan,
    string Status,
    decimal MonthlyAmount,
    DateTime StartedAt,
    DateTime? EndsAt);
