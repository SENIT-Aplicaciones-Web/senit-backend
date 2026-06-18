using Senit.Platform.API.GuestStay.Application.CommandServices;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.GuestStay.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.GuestStay.Domain.Model.Errors;
using Senit.Platform.API.Shared.Domain.Model.ValueObjects;

namespace Senit.Platform.API.GuestStay.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for consumption use cases.
/// </summary>
public class ConsumptionCommandService(
    IConsumptionRepository repository,
    IUnitOfWork unitOfWork) : IConsumptionCommandService
{
    public async Task<ApplicationResult<Consumption>> Handle(CreateConsumptionCommand command, CancellationToken cancellationToken = default)
    {
        if (command.UnitPrice < 0)
            return ApplicationResult<Consumption>.Failure(nameof(GuestStayErrors.InvalidAmount), StatusCodes.Status400BadRequest, 0);
        var entity = new Consumption(
            Guid.NewGuid().ToString(),
            command.GuestStayId,
            command.Description,
            command.Quantity,
            command.UnitPrice,
            command.Amount);

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Consumption>.Created(entity);
    }

    public async Task<ApplicationResult<Consumption>> Handle(UpdateConsumptionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<Consumption>.Failure(nameof(GuestStayErrors.ConsumptionNotFound), StatusCodes.Status404NotFound);

        if (command.UnitPrice < 0)
            return ApplicationResult<Consumption>.Failure(nameof(GuestStayErrors.InvalidAmount), StatusCodes.Status400BadRequest, 0);
        entity.Update(
            command.GuestStayId,
            command.Description,
            command.Quantity,
            command.UnitPrice,
            command.Amount);

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Consumption>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteConsumptionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(GuestStayErrors.ConsumptionNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
