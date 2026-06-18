using Senit.Platform.API.FrontDesk.Application.CommandServices;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.FrontDesk.Domain.Model.Commands;
using Senit.Platform.API.FrontDesk.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.FrontDesk.Domain.Model.Errors;

namespace Senit.Platform.API.FrontDesk.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for hotel use cases.
/// </summary>
public class HotelCommandService(
    IHotelRepository repository,
    IUnitOfWork unitOfWork) : IHotelCommandService
{
    public async Task<ApplicationResult<Hotel>> Handle(CreateHotelCommand command, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(command.Ruc) &&
            await repository.ExistsByRucAsync(command.Ruc, cancellationToken: cancellationToken))
            return ApplicationResult<Hotel>.Failure(nameof(FrontDeskErrors.DuplicateHotelRuc), StatusCodes.Status409Conflict);
        var entity = new Hotel(
            Guid.NewGuid().ToString(),
            command.Name,
            command.Ruc,
            command.Address,
            command.Phone,
            command.Email,
            command.Plan,
            command.Status);

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Hotel>.Created(entity);
    }

    public async Task<ApplicationResult<Hotel>> Handle(UpdateHotelCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<Hotel>.Failure(nameof(FrontDeskErrors.HotelNotFound), StatusCodes.Status404NotFound);

        if (!string.IsNullOrWhiteSpace(command.Ruc) &&
            await repository.ExistsByRucAsync(command.Ruc, command.Id, cancellationToken))
            return ApplicationResult<Hotel>.Failure(nameof(FrontDeskErrors.DuplicateHotelRuc), StatusCodes.Status409Conflict);
        entity.Update(
            command.Name,
            command.Ruc,
            command.Address,
            command.Phone,
            command.Email,
            command.Plan,
            command.Status);

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Hotel>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteHotelCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(FrontDeskErrors.HotelNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
