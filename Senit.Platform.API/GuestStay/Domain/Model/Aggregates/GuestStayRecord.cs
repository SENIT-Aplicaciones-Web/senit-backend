using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.GuestStay.Domain.Model.Aggregates;

/// <summary>
///     Represents a gueststay aggregate.
/// </summary>
public class GuestStayRecord : AuditableEntity
{
    public string HotelId { get; private set; } = string.Empty;
    public string RoomId { get; private set; } = string.Empty;
    public string GuestId { get; private set; } = string.Empty;
    public string GuestName { get; private set; } = string.Empty;
    public string? AdditionalGuestsJson { get; private set; }
    public DateTime StartAt { get; private set; }
    public DateTime ExpectedEndAt { get; private set; }
    public DateTime? ActualEndAt { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public decimal BaseAmount { get; private set; }
    public decimal AdditionalAmount { get; private set; }
    public decimal PrepaidAmount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string PaymentStatus { get; private set; } = string.Empty;

    public GuestStayRecord()
    {
    }

    public GuestStayRecord(
        string id,
        string hotelId,
        string roomId,
        string guestId,
        string guestName,
        string? additionalGuestsJson,
        DateTime startAt,
        DateTime expectedEndAt,
        DateTime? actualEndAt,
        string status,
        decimal baseAmount,
        decimal additionalAmount,
        decimal prepaidAmount,
        decimal totalAmount,
        string paymentStatus)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        HotelId = hotelId;
        RoomId = roomId;
        GuestId = guestId;
        GuestName = guestName;
        AdditionalGuestsJson = additionalGuestsJson;
        StartAt = startAt;
        ExpectedEndAt = expectedEndAt;
        ActualEndAt = actualEndAt;
        Status = status;
        BaseAmount = baseAmount;
        AdditionalAmount = additionalAmount;
        PrepaidAmount = prepaidAmount;
        TotalAmount = totalAmount;
        PaymentStatus = paymentStatus;
    }

    public void Update(
        string hotelId,
        string roomId,
        string guestId,
        string guestName,
        string? additionalGuestsJson,
        DateTime startAt,
        DateTime expectedEndAt,
        DateTime? actualEndAt,
        string status,
        decimal baseAmount,
        decimal additionalAmount,
        decimal prepaidAmount,
        decimal totalAmount,
        string paymentStatus)
    {
        HotelId = hotelId;
        RoomId = roomId;
        GuestId = guestId;
        GuestName = guestName;
        AdditionalGuestsJson = additionalGuestsJson;
        StartAt = startAt;
        ExpectedEndAt = expectedEndAt;
        ActualEndAt = actualEndAt;
        Status = status;
        BaseAmount = baseAmount;
        AdditionalAmount = additionalAmount;
        PrepaidAmount = prepaidAmount;
        TotalAmount = totalAmount;
        PaymentStatus = paymentStatus;
    }
}
