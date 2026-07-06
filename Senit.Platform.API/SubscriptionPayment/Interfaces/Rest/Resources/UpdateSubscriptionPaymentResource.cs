using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a subscriptionpayment.
/// </summary>
public class UpdateSubscriptionPaymentResource
{
    [Required(ErrorMessage = "Subscription identifier is required")]
    [StringLength(64, ErrorMessage = "Subscription identifier must not exceed 64 characters")]
    [OpenApiExample("subscription_basic_senit_lima")]
    public string SubscriptionId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Hotel identifier is required")]
    [StringLength(64, ErrorMessage = "Hotel identifier must not exceed 64 characters")]
    [OpenApiExample("hotel_senit_lima")]
    public string HotelId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Plan is required")]
    [RegularExpression("^(Basic|Pro)$", ErrorMessage = "Plan must be Basic or Pro")]
    [OpenApiExample("Basic")]
    public string Plan { get; init; } = string.Empty;

    [Range(typeof(decimal), "0.01", "99999999", ErrorMessage = "Amount must be greater than zero")]
    [OpenApiExample(49.00)]
    public decimal Amount { get; init; }

    [Required(ErrorMessage = "Payment method is required")]
    [RegularExpression("^(cash|card|transfer|yape)$", ErrorMessage = "Payment method must be cash card transfer or yape")]
    [OpenApiExample("card")]
    public string Method { get; init; } = string.Empty;

    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("^(pending|paid|cancelled)$", ErrorMessage = "Status must be pending paid or cancelled")]
    [OpenApiExample("paid")]
    public string Status { get; init; } = string.Empty;

    [OpenApiExample("2026-07-06T14:30:00Z")]
    public DateTime? PaidAt { get; init; }
}
