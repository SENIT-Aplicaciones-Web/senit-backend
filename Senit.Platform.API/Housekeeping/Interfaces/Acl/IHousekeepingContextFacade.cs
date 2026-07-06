namespace Senit.Platform.API.Housekeeping.Interfaces.Acl;

/// <summary>
///     Anti corruption facade exposed by the Housekeeping bounded context.
/// </summary>
public interface IHousekeepingContextFacade
{
    /// <summary>
    ///     Creates a cleaning task without exposing Housekeeping aggregates to other contexts.
    /// </summary>
    Task<string> CreateCleaningTask(
        string hotelId,
        string roomId,
        string description,
        string status,
        CancellationToken cancellationToken = default);
}
