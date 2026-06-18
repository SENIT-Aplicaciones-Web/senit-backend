namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned for a consumption.
/// </summary>
public record ConsumptionResource(
    string Id,
    string GuestStayId,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal Amount,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
