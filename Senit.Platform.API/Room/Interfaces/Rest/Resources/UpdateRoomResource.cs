using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.Room.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a room.
/// </summary>
public class UpdateRoomResource
{
    [Required(ErrorMessage = "Hotel identifier is required")]
    [StringLength(64, ErrorMessage = "Hotel identifier must not exceed 64 characters")]
    [OpenApiExample("hotel_senit_lima")]
    public string HotelId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Room number is required")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Room number must have 1 to 20 characters")]
    [OpenApiExample("101")]
    public string Number { get; init; } = string.Empty;

    [Range(1, 100, ErrorMessage = "Floor must be between 1 and 100")]
    [OpenApiExample(1)]
    public int Floor { get; init; }

    [Required(ErrorMessage = "Type is required")]
    [RegularExpression("^(Standard|Deluxe|Suite)$", ErrorMessage = "Type must be Standard Deluxe or Suite")]
    [OpenApiExample("Standard")]
    public string Type { get; init; } = string.Empty;

    [Range(1, 50, ErrorMessage = "Capacity must be between 1 and 50")]
    [OpenApiExample(2)]
    public int Capacity { get; init; }

    [Range(typeof(decimal), "0.01", "99999999", ErrorMessage = "Price per hour must be greater than zero")]
    [OpenApiExample(20.00)]
    public decimal PricePerHour { get; init; }

    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("^(available|cleaning|maintenance)$", ErrorMessage = "Status must be available cleaning or maintenance")]
    [OpenApiExample("available")]
    public string Status { get; init; } = string.Empty;
}
