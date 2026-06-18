namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a subscription.
/// </summary>
public class UpdateSubscriptionResource
{
    public string HotelId { get; init; } = string.Empty;

    public string Plan { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public decimal MonthlyAmount { get; init; }

    public DateTime StartedAt { get; init; }

    public DateTime? EndsAt { get; init; }

}
