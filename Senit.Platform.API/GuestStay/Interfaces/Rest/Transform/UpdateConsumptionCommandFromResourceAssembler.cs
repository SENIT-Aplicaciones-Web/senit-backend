using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;

/// <summary>
///     Converts an update consumption resource to a command.
/// </summary>
public static class UpdateConsumptionCommandFromResourceAssembler
{
    public static UpdateConsumptionCommand ToCommandFromResource(string id, UpdateConsumptionResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "UpdateConsumptionResource cannot be null when converting to command.");

        return new UpdateConsumptionCommand(
            id,
            resource.GuestStayId,
            resource.Description,
            resource.Quantity,
            resource.UnitPrice,
            resource.Amount);
    }
}
