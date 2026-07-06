using Senit.Platform.API.Housekeeping.Application.CommandServices;
using Senit.Platform.API.Housekeeping.Domain.Model.Commands;
using Senit.Platform.API.Housekeeping.Interfaces.Acl;

namespace Senit.Platform.API.Housekeeping.Application.Acl;

/// <summary>
///     Anti corruption facade implementation for the Housekeeping bounded context.
/// </summary>
public class HousekeepingContextFacade(
    ICleaningTaskCommandService cleaningTaskCommandService) : IHousekeepingContextFacade
{
    /// <summary>
    ///     Creates a cleaning task through the Housekeeping bounded context.
    /// </summary>
    public async Task<string> CreateCleaningTask(
        string hotelId,
        string roomId,
        string description,
        string status,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCleaningTaskCommand(hotelId, roomId, description, status);
        var result = await cleaningTaskCommandService.Handle(command, cancellationToken);
        return result.IsSuccess ? result.Value?.Id ?? string.Empty : string.Empty;
    }
}
