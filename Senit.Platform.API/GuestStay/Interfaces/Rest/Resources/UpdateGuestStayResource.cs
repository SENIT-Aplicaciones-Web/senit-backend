namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a gueststay.
/// </summary>
public class UpdateGuestStayResource
{
    public string HotelId { get; init; } = string.Empty;

    public string RoomId { get; init; } = string.Empty;

    public string GuestId { get; init; } = string.Empty;

    public string GuestName { get; init; } = string.Empty;

    public DateTime StartAt { get; init; }

    public DateTime ExpectedEndAt { get; init; }

    public DateTime? ActualEndAt { get; init; }

    public string Status { get; init; } = string.Empty;

    public decimal BaseAmount { get; init; }

    public decimal AdditionalAmount { get; init; }

    public decimal PrepaidAmount { get; init; }

    public decimal TotalAmount { get; init; }

    public string PaymentStatus { get; init; } = string.Empty;

}
