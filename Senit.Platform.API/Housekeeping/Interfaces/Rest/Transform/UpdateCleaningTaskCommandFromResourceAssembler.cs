using Senit.Platform.API.Housekeeping.Domain.Model.Commands;
using Senit.Platform.API.Housekeeping.Interfaces.Rest.Resources;

namespace Senit.Platform.API.Housekeeping.Interfaces.Rest.Transform;

/// <summary>
///     Converts update cleaning task resources to commands.
/// </summary>
public static class UpdateCleaningTaskCommandFromResourceAssembler
{
    public static UpdateCleaningTaskCommand ToCommandFromResource(string id, UpdateCleaningTaskResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "UpdateCleaningTaskResource cannot be null when converting to command.");

        return new UpdateCleaningTaskCommand(
            id,
            resource.HotelId,
            resource.RoomId,
            resource.Description,
            resource.Status);
    }
}
