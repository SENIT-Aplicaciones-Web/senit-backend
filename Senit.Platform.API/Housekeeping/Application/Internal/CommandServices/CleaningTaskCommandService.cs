using Senit.Platform.API.Housekeeping.Application.CommandServices;
using Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;
using Senit.Platform.API.Housekeeping.Domain.Model.Commands;
using Senit.Platform.API.Housekeeping.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.Housekeeping.Domain.Model.Errors;

namespace Senit.Platform.API.Housekeeping.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for cleaningtask use cases.
/// </summary>
public class CleaningTaskCommandService(
    ICleaningTaskRepository repository,
    IUnitOfWork unitOfWork) : ICleaningTaskCommandService
{
    public async Task<ApplicationResult<CleaningTask>> Handle(CreateCleaningTaskCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new CleaningTask(
            Guid.NewGuid().ToString(),
            command.HotelId,
            command.RoomId,
            command.Description,
            command.Status);

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<CleaningTask>.Created(entity);
    }

    public async Task<ApplicationResult<CleaningTask>> Handle(UpdateCleaningTaskCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<CleaningTask>.Failure(nameof(HousekeepingErrors.CleaningTaskNotFound), StatusCodes.Status404NotFound);

        entity.Update(
            command.HotelId,
            command.RoomId,
            command.Description,
            command.Status);

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<CleaningTask>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteCleaningTaskCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(HousekeepingErrors.CleaningTaskNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
