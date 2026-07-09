using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.Reservation.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a reservation.
/// </summary>
public class UpdateReservationResource
{
    [Required(ErrorMessage = "Hotel identifier is required")]
    [StringLength(64, ErrorMessage = "Hotel identifier must not exceed 64 characters")]
    [OpenApiExample("hotel_senit_lima")]
    public string HotelId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Room identifier is required")]
    [StringLength(64, ErrorMessage = "Room identifier must not exceed 64 characters")]
    [OpenApiExample("room_standard_101")]
    public string RoomId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Guest name is required")]
    [RegularExpression(@"^[\p{L}\s.'-]{3,80}$", ErrorMessage = "Guest name must have 3 to 80 valid characters")]
    [OpenApiExample("Juan Perez")]
    public string GuestName { get; init; } = string.Empty;

    [Required(ErrorMessage = "DNI is required")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "DNI must have exactly 8 digits")]
    [OpenApiExample("12345678")]
    public string Dni { get; init; } = string.Empty;

    [Required(ErrorMessage = "Phone is required")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone must have exactly 9 digits")]
    [OpenApiExample("987654321")]
    public string Phone { get; init; } = string.Empty;

    [EmailAddress(ErrorMessage = "Email must have a valid format")]
    [StringLength(250, ErrorMessage = "Email must not exceed 250 characters")]
    [OpenApiExample("juan.perez@mail.com")]
    public string? Email { get; init; }

    [Range(1, 50, ErrorMessage = "Guests quantity must be between 1 and 50")]
    [OpenApiExample(2)]
    public int GuestsQuantity { get; init; }

    [OpenApiExample("2026-07-06T14:00:00Z")]
    public DateTime StartAt { get; init; }

    [OpenApiExample("2026-07-06T18:00:00Z")]
    public DateTime EndAt { get; init; }

    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("^(confirmed|cancelled|completed)$", ErrorMessage = "Status must be confirmed cancelled or completed")]
    [OpenApiExample("confirmed")]
    public string Status { get; init; } = string.Empty;

    [Range(typeof(decimal), "1", "99999999", ErrorMessage = "Hours must be greater than zero", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    [OpenApiExample(4.00)]
    public decimal Hours { get; init; }

    [Range(typeof(decimal), "0", "99999999", ErrorMessage = "Reservation amount must be zero or greater", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    [OpenApiExample(80.00)]
    public decimal ReservationAmount { get; init; }

    [Range(typeof(decimal), "0", "99999999", ErrorMessage = "Prepaid amount must be zero or greater", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    [OpenApiExample(80.00)]
    public decimal PrepaidAmount { get; init; }

    [Required(ErrorMessage = "Payment method is required")]
    [RegularExpression("^(cash|card|transfer|yape)$", ErrorMessage = "Payment method must be cash card transfer or yape")]
    [OpenApiExample("cash")]
    public string PaymentMethod { get; init; } = string.Empty;

    [Required(ErrorMessage = "Payment status is required")]
    [RegularExpression("^(pending|paid)$", ErrorMessage = "Payment status must be pending or paid")]
    [OpenApiExample("paid")]
    public string PaymentStatus { get; init; } = string.Empty;

    [OpenApiExample("2026-07-06T14:30:00Z")]
    public DateTime? PaidAt { get; init; }
}
