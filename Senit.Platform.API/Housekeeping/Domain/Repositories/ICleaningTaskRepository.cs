using Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.Housekeeping.Domain.Repositories;

/// <summary>
///     Repository contract for cleaningtask entities.
/// </summary>
public interface ICleaningTaskRepository : IBaseRepository<CleaningTask>
{

}
