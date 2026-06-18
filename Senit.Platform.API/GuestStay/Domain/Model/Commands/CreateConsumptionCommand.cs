namespace Senit.Platform.API.GuestStay.Domain.Model.Commands;

/// <summary>
///     Command used to create a consumption.
/// </summary>
public record CreateConsumptionCommand(
    string GuestStayId,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal Amount);
