using Senit.Platform.API.Shared.Infrastructure.OpenApi;
using System.ComponentModel.DataAnnotations;

namespace Senit.Platform.API.Housekeeping.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create a cleaning task.
/// </summary>
public class CreateCleaningTaskResource
{
    [Required(ErrorMessage = "Hotel identifier is required")]
    [StringLength(64, ErrorMessage = "Hotel identifier must not exceed 64 characters")]
    [OpenApiExample("hotel_senit_lima")]
    public string HotelId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Room identifier is required")]
    [StringLength(64, ErrorMessage = "Room identifier must not exceed 64 characters")]
    [OpenApiExample("room_standard_101")]
    public string RoomId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(250, MinimumLength = 3, ErrorMessage = "Description must have 3 to 250 characters")]
    [OpenApiExample("Limpieza posterior al checkout")]
    public string Description { get; init; } = string.Empty;

    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("^(pending|completed)$", ErrorMessage = "Status must be pending or completed")]
    [OpenApiExample("pending")]
    public string Status { get; init; } = string.Empty;
}
