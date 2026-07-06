using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create a subscription.
/// </summary>
public class CreateSubscriptionResource
{
    [Required(ErrorMessage = "Hotel identifier is required")]
    [StringLength(64, ErrorMessage = "Hotel identifier must not exceed 64 characters")]
    [OpenApiExample("hotel_senit_lima")]
    public string HotelId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Plan is required")]
    [RegularExpression("^(Basic|Pro)$", ErrorMessage = "Plan must be Basic or Pro")]
    [OpenApiExample("Basic")]
    public string Plan { get; init; } = string.Empty;

    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("^(active|inactive|cancelled)$", ErrorMessage = "Status must be active inactive or cancelled")]
    [OpenApiExample("active")]
    public string Status { get; init; } = string.Empty;

    [Range(typeof(decimal), "0", "99999999", ErrorMessage = "Monthly amount must be zero or greater")]
    [OpenApiExample(49.00)]
    public decimal MonthlyAmount { get; init; }

    [OpenApiExample("2026-07-06T14:00:00Z")]
    public DateTime StartedAt { get; init; }

    [OpenApiExample(null)]
    public DateTime? EndsAt { get; init; }
}
