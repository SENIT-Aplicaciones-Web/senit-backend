namespace Senit.Platform.API.Payment.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a payment.
/// </summary>
public class UpdatePaymentResource
{
    public string HotelId { get; init; } = string.Empty;

    public string? GuestStayId { get; init; }

    public string? ReservationId { get; init; }

    public decimal Amount { get; init; }

    public string Method { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public DateTime? PaidAt { get; init; }

}
