using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a hotel.
/// </summary>
public class UpdateHotelResource
{
    [Required(ErrorMessage = "Hotel name is required")]
    [StringLength(250, MinimumLength = 3, ErrorMessage = "Hotel name must have 3 to 250 characters")]
    [OpenApiExample("Hotel Senit Lima")]
    public string Name { get; init; } = string.Empty;

    [StringLength(20, ErrorMessage = "RUC must not exceed 20 characters")]
    [RegularExpression(@"^$|^\d{11}$", ErrorMessage = "RUC must have 11 digits")]
    [OpenApiExample("20601234567")]
    public string Ruc { get; init; } = string.Empty;

    [StringLength(250, ErrorMessage = "Address must not exceed 250 characters")]
    [OpenApiExample("Av Arequipa 123 Lima")]
    public string Address { get; init; } = string.Empty;

    [StringLength(20, ErrorMessage = "Phone must not exceed 20 characters")]
    [RegularExpression(@"^$|^\d{9,20}$", ErrorMessage = "Phone must have 9 to 20 digits")]
    [OpenApiExample("987654321")]
    public string Phone { get; init; } = string.Empty;

    [EmailAddress(ErrorMessage = "Email must have a valid format")]
    [StringLength(250, ErrorMessage = "Email must not exceed 250 characters")]
    [OpenApiExample("contacto@senit.pe")]
    public string Email { get; init; } = string.Empty;

    [Required(ErrorMessage = "Plan is required")]
    [RegularExpression("^(Basic|Pro)$", ErrorMessage = "Plan must be Basic or Pro")]
    [OpenApiExample("Basic")]
    public string Plan { get; init; } = string.Empty;

    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("^(active|inactive)$", ErrorMessage = "Status must be active or inactive")]
    [OpenApiExample("active")]
    public string Status { get; init; } = string.Empty;
}
