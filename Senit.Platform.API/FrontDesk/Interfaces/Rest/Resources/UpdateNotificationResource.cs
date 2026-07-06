using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update an operational notification.
/// </summary>
public class UpdateNotificationResource
{
    [Required(ErrorMessage = "Hotel identifier is required")]
    [StringLength(64, ErrorMessage = "Hotel identifier must not exceed 64 characters")]
    [OpenApiExample("hotel_senit_lima")]
    public string HotelId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Title is required")]
    [StringLength(250, MinimumLength = 3, ErrorMessage = "Title must have 3 to 250 characters")]
    [OpenApiExample("Checkout pendiente")]
    public string Title { get; init; } = string.Empty;

    [Required(ErrorMessage = "Message is required")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "Message must have 3 to 500 characters")]
    [OpenApiExample("La habitación 204 tiene checkout pendiente")]
    public string Message { get; init; } = string.Empty;

    [Required(ErrorMessage = "Type is required")]
    [RegularExpression("^(danger|warning|success|overdue|soon|done|info)$", ErrorMessage = "Type must be danger warning success overdue soon done or info")]
    [OpenApiExample("warning")]
    public string Type { get; init; } = string.Empty;

    [StringLength(64, ErrorMessage = "Created by must not exceed 64 characters")]
    [OpenApiExample("user_maria_rodriguez")]
    public string? CreatedBy { get; init; }
}
