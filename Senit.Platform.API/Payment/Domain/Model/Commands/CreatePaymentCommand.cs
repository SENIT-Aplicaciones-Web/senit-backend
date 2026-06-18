namespace Senit.Platform.API.Payment.Domain.Model.Commands;

/// <summary>
///     Command used to create a payment.
/// </summary>
public record CreatePaymentCommand(
    string HotelId,
    string? GuestStayId,
    string? ReservationId,
    decimal Amount,
    string Method,
    string Status,
    DateTime? PaidAt);
