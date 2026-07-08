using System.ComponentModel.DataAnnotations;
using Senit.Platform.API.Shared.Infrastructure.OpenApi;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create a Stripe hosted Checkout session for a subscription.
/// </summary>
public class CreateStripeCheckoutSessionResource
{
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
    [MinLength(6, ErrorMessage = "Password must have at least 6 characters")]
    [StringLength(250, ErrorMessage = "Password must not exceed 250 characters")]
    [OpenApiExample("Recepcion123")]
    public string Password { get; init; } = string.Empty;

    [Required(ErrorMessage = "Plan is required")]
    [RegularExpression("^(Basic|Pro)$", ErrorMessage = "Plan must be Basic or Pro")]
    [OpenApiExample("Pro")]
    public string Plan { get; init; } = "Basic";
}
