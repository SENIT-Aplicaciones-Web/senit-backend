namespace Senit.Platform.API.Payment.Domain.Model.Commands;

/// <summary>
///     Command used to update a payment.
/// </summary>
public record UpdatePaymentCommand(
    string Id,
    string HotelId,
    string? GuestStayId,
    string? ReservationId,
    decimal Amount,
    string Method,
    string Status,
    DateTime? PaidAt);
