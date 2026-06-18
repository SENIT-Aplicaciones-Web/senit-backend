using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;

/// <summary>
///     Converts a create consumption resource to a command.
/// </summary>
public static class CreateConsumptionCommandFromResourceAssembler
{
    public static CreateConsumptionCommand ToCommandFromResource(CreateConsumptionResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(
                nameof(resource),
                "CreateConsumptionResource cannot be null when converting to command.");

        return new CreateConsumptionCommand(
            resource.GuestStayId,
            resource.Description,
            resource.Quantity,
            resource.UnitPrice,
            resource.Amount);
    }
}
