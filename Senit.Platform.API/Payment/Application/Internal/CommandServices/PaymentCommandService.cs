using Senit.Platform.API.Payment.Application.CommandServices;
using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Domain.Model.Commands;
using Senit.Platform.API.Payment.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.Payment.Domain.Model.Errors;
using Senit.Platform.API.Shared.Domain.Model.ValueObjects;

namespace Senit.Platform.API.Payment.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for payment use cases.
/// </summary>
public class PaymentCommandService(
    IPaymentRepository repository,
    IUnitOfWork unitOfWork) : IPaymentCommandService
{
    public async Task<ApplicationResult<PaymentRecord>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken = default)
    {
        if (command.Amount < 0)
            return ApplicationResult<PaymentRecord>.Failure(nameof(PaymentErrors.InvalidAmount), StatusCodes.Status400BadRequest, 0);
        var entity = new PaymentRecord(
            Guid.NewGuid().ToString(),
            command.HotelId,
            command.GuestStayId,
            command.ReservationId,
            command.Amount,
            command.Method,
            command.Status,
            command.PaidAt);

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<PaymentRecord>.Created(entity);
    }

    public async Task<ApplicationResult<PaymentRecord>> Handle(UpdatePaymentCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<PaymentRecord>.Failure(nameof(PaymentErrors.PaymentNotFound), StatusCodes.Status404NotFound);

        if (command.Amount < 0)
            return ApplicationResult<PaymentRecord>.Failure(nameof(PaymentErrors.InvalidAmount), StatusCodes.Status400BadRequest, 0);
        entity.Update(
            command.HotelId,
            command.GuestStayId,
            command.ReservationId,
            command.Amount,
            command.Method,
            command.Status,
            command.PaidAt);

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<PaymentRecord>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeletePaymentCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(PaymentErrors.PaymentNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
