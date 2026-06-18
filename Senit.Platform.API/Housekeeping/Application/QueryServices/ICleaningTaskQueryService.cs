using Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;
using Senit.Platform.API.Housekeeping.Domain.Model.Queries;

namespace Senit.Platform.API.Housekeeping.Application.QueryServices;

/// <summary>
///     Query service contract for cleaningtask use cases.
/// </summary>
public interface ICleaningTaskQueryService
{
    Task<IEnumerable<CleaningTask>> Handle(GetAllCleaningTasksQuery query, CancellationToken cancellationToken = default);

    Task<CleaningTask?> Handle(GetCleaningTaskByIdQuery query, CancellationToken cancellationToken = default);
}
