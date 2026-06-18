namespace Senit.Platform.API.Reservation.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a reservation.
/// </summary>
public class UpdateReservationResource
{
    public string HotelId { get; init; } = string.Empty;

    public string RoomId { get; init; } = string.Empty;

    public string GuestName { get; init; } = string.Empty;

    public string Dni { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public string? Email { get; init; }

    public int GuestsQuantity { get; init; }

    public DateTime StartAt { get; init; }

    public DateTime EndAt { get; init; }

    public string Status { get; init; } = string.Empty;

    public decimal Hours { get; init; }

    public decimal ReservationAmount { get; init; }

    public decimal PrepaidAmount { get; init; }

    public string PaymentMethod { get; init; } = string.Empty;

    public string PaymentStatus { get; init; } = string.Empty;

    public DateTime? PaidAt { get; init; }

}
