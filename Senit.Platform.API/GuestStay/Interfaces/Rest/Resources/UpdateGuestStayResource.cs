using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a gueststay.
/// </summary>
public class UpdateGuestStayResource
{
    [Required(ErrorMessage = "Hotel identifier is required")]
    [StringLength(64, ErrorMessage = "Hotel identifier must not exceed 64 characters")]
    [OpenApiExample("hotel_senit_lima")]
    public string HotelId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Room identifier is required")]
    [StringLength(64, ErrorMessage = "Room identifier must not exceed 64 characters")]
    [OpenApiExample("room_standard_101")]
    public string RoomId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Guest identifier is required")]
    [StringLength(64, ErrorMessage = "Guest identifier must not exceed 64 characters")]
    [OpenApiExample("guest_juan_perez")]
    public string GuestId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Guest name is required")]
    [StringLength(250, MinimumLength = 3, ErrorMessage = "Guest name must have 3 to 250 characters")]
    [OpenApiExample("Juan Perez")]
    public string GuestName { get; init; } = string.Empty;

    [StringLength(4000, ErrorMessage = "Additional guests data must not exceed 4000 characters")]
    [OpenApiExample("[{\"fullName\":\"Ana Torres\",\"dni\":\"87654321\"}]")]
    public string? AdditionalGuestsJson { get; init; }

    [OpenApiExample("2026-07-06T14:00:00Z")]
    public DateTime StartAt { get; init; }

    [OpenApiExample("2026-07-06T18:00:00Z")]
    public DateTime ExpectedEndAt { get; init; }

    [OpenApiExample(null)]
    public DateTime? ActualEndAt { get; init; }

    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("^(active|finished|cancelled)$", ErrorMessage = "Status must be active finished or cancelled")]
    [OpenApiExample("active")]
    public string Status { get; init; } = string.Empty;

    [Range(typeof(decimal), "0", "99999999", ErrorMessage = "Base amount must be zero or greater", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    [OpenApiExample(80.00)]
    public decimal BaseAmount { get; init; }

    [Range(typeof(decimal), "0", "99999999", ErrorMessage = "Additional amount must be zero or greater", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    [OpenApiExample(0.00)]
    public decimal AdditionalAmount { get; init; }

    [Range(typeof(decimal), "0", "99999999", ErrorMessage = "Prepaid amount must be zero or greater", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    [OpenApiExample(0.00)]
    public decimal PrepaidAmount { get; init; }

    [Range(typeof(decimal), "0", "99999999", ErrorMessage = "Total amount must be zero or greater", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    [OpenApiExample(80.00)]
    public decimal TotalAmount { get; init; }

    [Required(ErrorMessage = "Payment status is required")]
    [RegularExpression("^(pending|paid)$", ErrorMessage = "Payment status must be pending or paid")]
    [OpenApiExample("pending")]
    public string PaymentStatus { get; init; } = string.Empty;
}
