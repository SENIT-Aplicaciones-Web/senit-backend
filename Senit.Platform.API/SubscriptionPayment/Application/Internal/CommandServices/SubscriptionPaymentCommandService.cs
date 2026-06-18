using Senit.Platform.API.SubscriptionPayment.Application.CommandServices;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.SubscriptionPayment.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Errors;
using Senit.Platform.API.Shared.Domain.Model.ValueObjects;

namespace Senit.Platform.API.SubscriptionPayment.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for subscriptionpayment use cases.
/// </summary>
public class SubscriptionPaymentCommandService(
    ISubscriptionPaymentRepository repository,
    IUnitOfWork unitOfWork) : ISubscriptionPaymentCommandService
{
    public async Task<ApplicationResult<SubscriptionPaymentRecord>> Handle(CreateSubscriptionPaymentCommand command, CancellationToken cancellationToken = default)
    {
        if (command.Amount < 0)
            return ApplicationResult<SubscriptionPaymentRecord>.Failure(nameof(SubscriptionPaymentErrors.InvalidAmount), StatusCodes.Status400BadRequest, 0);
        var entity = new SubscriptionPaymentRecord(
            Guid.NewGuid().ToString(),
            command.SubscriptionId,
            command.HotelId,
            command.Plan,
            command.Amount,
            command.Method,
            command.Status,
            command.PaidAt);

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<SubscriptionPaymentRecord>.Created(entity);
    }

    public async Task<ApplicationResult<SubscriptionPaymentRecord>> Handle(UpdateSubscriptionPaymentCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<SubscriptionPaymentRecord>.Failure(nameof(SubscriptionPaymentErrors.SubscriptionPaymentNotFound), StatusCodes.Status404NotFound);

        if (command.Amount < 0)
            return ApplicationResult<SubscriptionPaymentRecord>.Failure(nameof(SubscriptionPaymentErrors.InvalidAmount), StatusCodes.Status400BadRequest, 0);
        entity.Update(
            command.SubscriptionId,
            command.HotelId,
            command.Plan,
            command.Amount,
            command.Method,
            command.Status,
            command.PaidAt);

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<SubscriptionPaymentRecord>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteSubscriptionPaymentCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(SubscriptionPaymentErrors.SubscriptionPaymentNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
