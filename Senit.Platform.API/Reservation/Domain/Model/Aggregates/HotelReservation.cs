using Senit.Platform.API.Shared.Domain.Model.Entities;

namespace Senit.Platform.API.Reservation.Domain.Model.Aggregates;

/// <summary>
///     Represents a reservation aggregate.
/// </summary>
public class HotelReservation : AuditableEntity
{
    public string HotelId { get; private set; } = string.Empty;
    public string RoomId { get; private set; } = string.Empty;
    public string GuestName { get; private set; } = string.Empty;
    public string Dni { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public int GuestsQuantity { get; private set; }
    public string? AdditionalGuestsJson { get; private set; }
    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public decimal Hours { get; private set; }
    public decimal ReservationAmount { get; private set; }
    public decimal PrepaidAmount { get; private set; }
    public string PaymentMethod { get; private set; } = string.Empty;
    public string PaymentStatus { get; private set; } = string.Empty;
    public DateTime? PaidAt { get; private set; }

    public HotelReservation()
    {
    }

    public HotelReservation(
        string id,
        string hotelId,
        string roomId,
        string guestName,
        string dni,
        string phone,
        string? email,
        int guestsQuantity,
        string? additionalGuestsJson,
        DateTime startAt,
        DateTime endAt,
        string status,
        decimal hours,
        decimal reservationAmount,
        decimal prepaidAmount,
        string paymentMethod,
        string paymentStatus,
        DateTime? paidAt)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        HotelId = hotelId;
        RoomId = roomId;
        GuestName = guestName;
        Dni = dni;
        Phone = phone;
        Email = email;
        GuestsQuantity = guestsQuantity;
        AdditionalGuestsJson = additionalGuestsJson;
        StartAt = startAt;
        EndAt = endAt;
        Status = status;
        Hours = hours;
        ReservationAmount = reservationAmount;
        PrepaidAmount = prepaidAmount;
        PaymentMethod = paymentMethod;
        PaymentStatus = paymentStatus;
        PaidAt = paidAt;
    }

    public void Update(
        string hotelId,
        string roomId,
        string guestName,
        string dni,
        string phone,
        string? email,
        int guestsQuantity,
        string? additionalGuestsJson,
        DateTime startAt,
        DateTime endAt,
        string status,
        decimal hours,
        decimal reservationAmount,
        decimal prepaidAmount,
        string paymentMethod,
        string paymentStatus,
        DateTime? paidAt)
    {
        HotelId = hotelId;
        RoomId = roomId;
        GuestName = guestName;
        Dni = dni;
        Phone = phone;
        Email = email;
        GuestsQuantity = guestsQuantity;
        AdditionalGuestsJson = additionalGuestsJson;
        StartAt = startAt;
        EndAt = endAt;
        Status = status;
        Hours = hours;
        ReservationAmount = reservationAmount;
        PrepaidAmount = prepaidAmount;
        PaymentMethod = paymentMethod;
        PaymentStatus = paymentStatus;
        PaidAt = paidAt;
    }
}
