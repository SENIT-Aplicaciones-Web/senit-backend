namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to update a consumption.
/// </summary>
public class UpdateConsumptionResource
{
    public string GuestStayId { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public int Quantity { get; init; }

    public decimal UnitPrice { get; init; }

    public decimal Amount { get; init; }

}
