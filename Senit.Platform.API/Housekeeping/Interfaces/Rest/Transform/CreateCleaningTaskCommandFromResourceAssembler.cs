using Senit.Platform.API.Housekeeping.Domain.Model.Commands;
using Senit.Platform.API.Housekeeping.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Housekeeping.Interfaces.Rest.Transform;

/// <summary>
///     Converts create cleaning task resources to commands.
/// </summary>
public static class CreateCleaningTaskCommandFromResourceAssembler
{
    public static CreateCleaningTaskCommand ToCommandFromResource(CreateCleaningTaskResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateCleaningTaskResource cannot be null when converting to command.");

        return new CreateCleaningTaskCommand(
            resource.HotelId,
            resource.RoomId,
            resource.Description,
            resource.Status);
    }
}
