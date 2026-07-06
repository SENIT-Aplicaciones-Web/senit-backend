using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.Payment.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a payment.
/// </summary>
public class UpdatePaymentResource
{
    [Required(ErrorMessage = "Hotel identifier is required")]
    [StringLength(64, ErrorMessage = "Hotel identifier must not exceed 64 characters")]
    [OpenApiExample("hotel_senit_lima")]
    public string HotelId { get; init; } = string.Empty;

    [StringLength(64, ErrorMessage = "Guest stay identifier must not exceed 64 characters")]
    [OpenApiExample("stay_room_deluxe_204_july")]
    public string? GuestStayId { get; init; }

    [StringLength(64, ErrorMessage = "Reservation identifier must not exceed 64 characters")]
    [OpenApiExample(null)]
    public string? ReservationId { get; init; }

    [Range(typeof(decimal), "0.01", "99999999", ErrorMessage = "Amount must be greater than zero", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    [OpenApiExample(80.00)]
    public decimal Amount { get; init; }

    [Required(ErrorMessage = "Payment method is required")]
    [RegularExpression("^(cash|card|transfer|yape)$", ErrorMessage = "Payment method must be cash card transfer or yape")]
    [OpenApiExample("cash")]
    public string Method { get; init; } = string.Empty;

    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("^(pending|paid|cancelled)$", ErrorMessage = "Status must be pending paid or cancelled")]
    [OpenApiExample("paid")]
    public string Status { get; init; } = string.Empty;

    [OpenApiExample("2026-07-06T14:30:00Z")]
    public DateTime? PaidAt { get; init; }
}
