using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource used by the public password reset flow.
/// </summary>
public class ResetPasswordResource
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email must have a valid format")]
    [StringLength(250, ErrorMessage = "Email must not exceed 250 characters")]
    [OpenApiExample("recepcion@senit.pe")]
    public string Email { get; init; } = string.Empty;

    [Required(ErrorMessage = "New password is required")]
    [MinLength(6, ErrorMessage = "New password must have at least 6 characters")]
    [StringLength(250, ErrorMessage = "New password must not exceed 250 characters")]
    [OpenApiExample("NuevaClave123")]
    public string NewPassword { get; init; } = string.Empty;
}
