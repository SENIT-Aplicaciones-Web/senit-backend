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
///     Command service implementation for subscription use cases.
/// </summary>
public class SubscriptionCommandService(
    ISubscriptionRepository repository,
    IUnitOfWork unitOfWork) : ISubscriptionCommandService
{
    public async Task<ApplicationResult<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken = default)
    {
        if (command.MonthlyAmount < 0)
            return ApplicationResult<Subscription>.Failure(nameof(SubscriptionPaymentErrors.InvalidAmount), StatusCodes.Status400BadRequest, 0);
        if (command.Plan is not "Basic" and not "Pro")
            return ApplicationResult<Subscription>.Failure(nameof(SubscriptionPaymentErrors.InvalidPlan), StatusCodes.Status400BadRequest);
        var entity = new Subscription(
            Guid.NewGuid().ToString(),
            command.HotelId,
            command.Plan,
            command.Status,
            command.MonthlyAmount,
            command.StartedAt,
            command.EndsAt);

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Subscription>.Created(entity);
    }

    public async Task<ApplicationResult<Subscription>> Handle(UpdateSubscriptionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<Subscription>.Failure(nameof(SubscriptionPaymentErrors.SubscriptionNotFound), StatusCodes.Status404NotFound);

        if (command.MonthlyAmount < 0)
            return ApplicationResult<Subscription>.Failure(nameof(SubscriptionPaymentErrors.InvalidAmount), StatusCodes.Status400BadRequest, 0);
        if (command.Plan is not "Basic" and not "Pro")
            return ApplicationResult<Subscription>.Failure(nameof(SubscriptionPaymentErrors.InvalidPlan), StatusCodes.Status400BadRequest);
        entity.Update(
            command.HotelId,
            command.Plan,
            command.Status,
            command.MonthlyAmount,
            command.StartedAt,
            command.EndsAt);

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Subscription>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteSubscriptionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(SubscriptionPaymentErrors.SubscriptionNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
