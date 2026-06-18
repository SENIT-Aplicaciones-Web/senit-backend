namespace Senit.Platform.API.GuestStay.Domain.Model.Commands;

/// <summary>
///     Command used to update a consumption.
/// </summary>
public record UpdateConsumptionCommand(
    string Id,
    string GuestStayId,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal Amount);
