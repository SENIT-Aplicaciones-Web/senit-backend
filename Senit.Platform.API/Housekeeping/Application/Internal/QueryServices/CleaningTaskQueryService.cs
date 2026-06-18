using Senit.Platform.API.Housekeeping.Application.QueryServices;
using Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;
using Senit.Platform.API.Housekeeping.Domain.Model.Queries;
using Senit.Platform.API.Housekeeping.Domain.Repositories;

namespace Senit.Platform.API.Housekeeping.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for cleaningtask use cases.
/// </summary>
public class CleaningTaskQueryService(ICleaningTaskRepository repository) : ICleaningTaskQueryService
{
    public async Task<IEnumerable<CleaningTask>> Handle(GetAllCleaningTasksQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.ListAsync(cancellationToken);
    }

    public async Task<CleaningTask?> Handle(GetCleaningTaskByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(query.Id, cancellationToken);
    }
}
