namespace Senit.Platform.API.Payment.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a payment.
/// </summary>
public record PaymentResource(
    string Id,
    string HotelId,
    string? GuestStayId,
    string? ReservationId,
    decimal Amount,
    string Method,
    string Status,
    DateTime? PaidAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
