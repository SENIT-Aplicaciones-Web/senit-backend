namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a subscriptionpayment.
/// </summary>
public class UpdateSubscriptionPaymentResource
{
    public string SubscriptionId { get; init; } = string.Empty;

    public string HotelId { get; init; } = string.Empty;

    public string Plan { get; init; } = string.Empty;

    public decimal Amount { get; init; }

    public string Method { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public DateTime? PaidAt { get; init; }

}
