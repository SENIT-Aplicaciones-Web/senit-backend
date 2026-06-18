using Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;
using Senit.Platform.API.Housekeeping.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.Housekeeping.Application.CommandServices;

/// <summary>
///     Command service contract for cleaningtask use cases.
/// </summary>
public interface ICleaningTaskCommandService
{
    Task<ApplicationResult<CleaningTask>> Handle(CreateCleaningTaskCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<CleaningTask>> Handle(UpdateCleaningTaskCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteCleaningTaskCommand command, CancellationToken cancellationToken = default);
}
