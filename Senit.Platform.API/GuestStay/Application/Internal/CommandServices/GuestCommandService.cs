using Senit.Platform.API.GuestStay.Application.CommandServices;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.GuestStay.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.GuestStay.Domain.Model.Errors;

namespace Senit.Platform.API.GuestStay.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for guest use cases.
/// </summary>
public class GuestCommandService(
    IGuestRepository repository,
    IUnitOfWork unitOfWork) : IGuestCommandService
{
    public async Task<ApplicationResult<Guest>> Handle(CreateGuestCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new Guest(
            Guid.NewGuid().ToString(),
            command.FullName,
            command.Dni,
            command.Phone,
            command.Email);

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Guest>.Created(entity);
    }

    public async Task<ApplicationResult<Guest>> Handle(UpdateGuestCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<Guest>.Failure(nameof(GuestStayErrors.GuestNotFound), StatusCodes.Status404NotFound);

        entity.Update(
            command.FullName,
            command.Dni,
            command.Phone,
            command.Email);

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Guest>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteGuestCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(GuestStayErrors.GuestNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
