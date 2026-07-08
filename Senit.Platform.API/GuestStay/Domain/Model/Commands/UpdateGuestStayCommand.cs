namespace Senit.Platform.API.GuestStay.Domain.Model.Commands;

/// <summary>
///     Command used to update a gueststay.
/// </summary>
public record UpdateGuestStayCommand(
    string Id,
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
