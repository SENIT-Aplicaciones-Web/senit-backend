using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create a user.
/// </summary>
public class CreateUserResource
{
    [Required(ErrorMessage = "Hotel identifier is required")]
    [StringLength(64, ErrorMessage = "Hotel identifier must not exceed 64 characters")]
    [OpenApiExample("hotel_senit_lima")]
    public string HotelId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(250, MinimumLength = 3, ErrorMessage = "Full name must have 3 to 250 characters")]
    [OpenApiExample("Maria Rodriguez")]
    public string FullName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Username is required")]
    [RegularExpression(@"^[A-Za-z0-9_]{3,100}$", ErrorMessage = "Username must use 3 to 100 letters numbers or underscores")]
    [OpenApiExample("mrodriguez")]
    public string Username { get; init; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email must have a valid format")]
    [StringLength(250, ErrorMessage = "Email must not exceed 250 characters")]
    [OpenApiExample("maria.rodriguez@senit.pe")]
    public string Email { get; init; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(4, ErrorMessage = "Password must have at least 4 characters")]
    [StringLength(250, ErrorMessage = "Password must not exceed 250 characters")]
    [OpenApiExample("Recepcion123")]
    public string Password { get; init; } = string.Empty;

    [Required(ErrorMessage = "Role is required")]
    [RegularExpression("^(ADMIN|FRONT_DESK)$", ErrorMessage = "Role must be ADMIN or FRONT_DESK")]
    [OpenApiExample("FRONT_DESK")]
    public string Role { get; init; } = string.Empty;

    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("^(active|inactive)$", ErrorMessage = "Status must be active or inactive")]
    [OpenApiExample("active")]
    public string Status { get; init; } = string.Empty;
}
