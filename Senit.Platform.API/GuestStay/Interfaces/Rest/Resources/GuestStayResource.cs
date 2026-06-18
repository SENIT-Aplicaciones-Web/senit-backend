namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a gueststay.
/// </summary>
public record GuestStayResource(
    string Id,
    string HotelId,
    string RoomId,
    string GuestId,
    string GuestName,
    DateTime StartAt,
    DateTime ExpectedEndAt,
    DateTime? ActualEndAt,
    string Status,
    decimal BaseAmount,
    decimal AdditionalAmount,
    decimal PrepaidAmount,
    decimal TotalAmount,
    string PaymentStatus,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
