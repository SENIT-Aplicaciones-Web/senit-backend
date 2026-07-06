using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create a consumption.
/// </summary>
public class CreateConsumptionResource
{
    [Required(ErrorMessage = "Guest stay identifier is required")]
    [StringLength(64, ErrorMessage = "Guest stay identifier must not exceed 64 characters")]
    [OpenApiExample("stay_room_deluxe_204_july")]
    public string GuestStayId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(250, MinimumLength = 3, ErrorMessage = "Description must have 3 to 250 characters")]
    [OpenApiExample("Agua mineral")]
    public string Description { get; init; } = string.Empty;

    [Range(1, 999, ErrorMessage = "Quantity must be between 1 and 999")]
    [OpenApiExample(2)]
    public int Quantity { get; init; }

    [Range(typeof(decimal), "0.01", "99999999", ErrorMessage = "Unit price must be greater than zero", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    [OpenApiExample(5.50)]
    public decimal UnitPrice { get; init; }

    [Range(typeof(decimal), "0.01", "99999999", ErrorMessage = "Amount must be greater than zero", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    [OpenApiExample(11.00)]
    public decimal Amount { get; init; }
}
