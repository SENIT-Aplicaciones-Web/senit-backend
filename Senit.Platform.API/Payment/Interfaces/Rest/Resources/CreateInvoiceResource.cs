using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.Payment.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create a invoice.
/// </summary>
public class CreateInvoiceResource
{
    [Required(ErrorMessage = "Payment identifier is required")]
    [StringLength(64, ErrorMessage = "Payment identifier must not exceed 64 characters")]
    [OpenApiExample("payment_yape_reservation")]
    public string PaymentId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Invoice number is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Invoice number must have 3 to 50 characters")]
    [OpenApiExample("F001 000001")]
    public string Number { get; init; } = string.Empty;

    [Required(ErrorMessage = "Customer name is required")]
    [StringLength(250, MinimumLength = 3, ErrorMessage = "Customer name must have 3 to 250 characters")]
    [OpenApiExample("Juan Perez")]
    public string CustomerName { get; init; } = string.Empty;

    [Range(typeof(decimal), "0.01", "99999999", ErrorMessage = "Amount must be greater than zero")]
    [OpenApiExample(80.00)]
    public decimal Amount { get; init; }

    [OpenApiExample("2026-07-06T14:40:00Z")]
    public DateTime IssuedAt { get; init; }
}
