using Senit.Platform.API.FrontDesk.Application.CommandServices;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.FrontDesk.Domain.Model.Commands;
using Senit.Platform.API.FrontDesk.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.FrontDesk.Domain.Model.Errors;

namespace Senit.Platform.API.FrontDesk.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for notification use cases.
/// </summary>
public class NotificationCommandService(
    INotificationRepository repository,
    IUnitOfWork unitOfWork) : INotificationCommandService
{
    public async Task<ApplicationResult<Notification>> Handle(CreateNotificationCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new Notification(
            Guid.NewGuid().ToString(),
            command.HotelId,
            command.Title,
            command.Message,
            command.Type,
            command.CreatedBy);

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Notification>.Created(entity);
    }

    public async Task<ApplicationResult<Notification>> Handle(UpdateNotificationCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<Notification>.Failure(nameof(FrontDeskErrors.NotificationNotFound), StatusCodes.Status404NotFound);

        entity.Update(
            command.HotelId,
            command.Title,
            command.Message,
            command.Type,
            command.CreatedBy);

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Notification>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteNotificationCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(FrontDeskErrors.NotificationNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
