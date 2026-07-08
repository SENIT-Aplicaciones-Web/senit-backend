namespace Senit.Platform.API.GuestStay.Domain.Model.Commands;

/// <summary>
///     Command used to create a gueststay.
/// </summary>
public record CreateGuestStayCommand(
    string HotelId,
    string RoomId,
    string GuestId,
    string GuestName,
    string? AdditionalGuestsJson,
    DateTime StartAt,
    DateTime ExpectedEndAt,
    DateTime? ActualEndAt,
    string Status,
    decimal BaseAmount,
    decimal AdditionalAmount,
    decimal PrepaidAmount,
    decimal TotalAmount,
    string PaymentStatus);
