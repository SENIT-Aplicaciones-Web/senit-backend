using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a guest.
/// </summary>
public class UpdateGuestResource
{
    [Required(ErrorMessage = "Full name is required")]
    [RegularExpression(@"^[\p{L}\s.'-]{3,80}$", ErrorMessage = "Full name must have 3 to 80 valid characters")]
    [OpenApiExample("Juan Perez")]
    public string FullName { get; init; } = string.Empty;

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
}
