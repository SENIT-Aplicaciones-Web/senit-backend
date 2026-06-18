using Senit.Platform.API.GuestStay.Domain.Repositories;
using Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;
using Senit.Platform.API.Housekeeping.Domain.Repositories;
using Senit.Platform.API.Payment.Application.CommandServices;
using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Domain.Model.Commands;
using Senit.Platform.API.Payment.Domain.Model.Errors;
using Senit.Platform.API.Payment.Domain.Repositories;
using Senit.Platform.API.Room.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.Payment.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for invoice use cases.
/// </summary>
public class InvoiceCommandService(
    IInvoiceRepository repository,
    IPaymentRepository paymentRepository,
    IGuestStayRepository guestStayRepository,
    IRoomRepository roomRepository,
    ICleaningTaskRepository cleaningTaskRepository,
    IUnitOfWork unitOfWork) : IInvoiceCommandService
{
    public async Task<ApplicationResult<Invoice>> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken = default)
    {
        if (command.Amount < 0)
            return ApplicationResult<Invoice>.Failure(nameof(PaymentErrors.InvalidAmount), StatusCodes.Status400BadRequest, 0);

        var payment = await paymentRepository.FindByIdAsync(command.PaymentId, cancellationToken);
        if (payment == null)
            return ApplicationResult<Invoice>.Failure(nameof(PaymentErrors.PaymentNotFound), StatusCodes.Status404NotFound);

        var entity = new Invoice(
            Guid.NewGuid().ToString(),
            command.PaymentId,
            command.Number,
            command.CustomerName,
            command.Amount,
            command.IssuedAt);

        await repository.AddAsync(entity, cancellationToken);

        if (!string.IsNullOrWhiteSpace(payment.GuestStayId))
        {
            var guestStay = await guestStayRepository.FindByIdAsync(payment.GuestStayId, cancellationToken);
            if (guestStay == null)
                return ApplicationResult<Invoice>.Failure(nameof(PaymentErrors.PaymentNotFound), StatusCodes.Status404NotFound);

            var actualEndAt = command.IssuedAt == default ? DateTime.UtcNow : command.IssuedAt;

            guestStay.Update(
                guestStay.HotelId,
                guestStay.RoomId,
                guestStay.GuestId,
                guestStay.GuestName,
                guestStay.StartAt,
                guestStay.ExpectedEndAt,
                actualEndAt,
                "finished",
                guestStay.BaseAmount,
                guestStay.AdditionalAmount,
                guestStay.PrepaidAmount,
                guestStay.TotalAmount,
                "paid");
            guestStayRepository.Update(guestStay);

            var room = await roomRepository.FindByIdAsync(guestStay.RoomId, cancellationToken);
            if (room != null)
            {
                room.Update(
                    room.HotelId,
                    room.Number,
                    room.Floor,
                    room.Type,
                    room.Capacity,
                    room.PricePerHour,
                    "cleaning");
                roomRepository.Update(room);
            }

            var cleaningTask = new CleaningTask(
                Guid.NewGuid().ToString(),
                guestStay.HotelId,
                guestStay.RoomId,
                "Limpieza posterior a checkout",
                "pending");
            await cleaningTaskRepository.AddAsync(cleaningTask, cancellationToken);
        }

        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Invoice>.Created(entity);
    }

    public async Task<ApplicationResult<Invoice>> Handle(UpdateInvoiceCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<Invoice>.Failure(nameof(PaymentErrors.InvoiceNotFound), StatusCodes.Status404NotFound);

        if (command.Amount < 0)
            return ApplicationResult<Invoice>.Failure(nameof(PaymentErrors.InvalidAmount), StatusCodes.Status400BadRequest, 0);
        entity.Update(
            command.PaymentId,
            command.Number,
            command.CustomerName,
            command.Amount,
            command.IssuedAt);

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<Invoice>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteInvoiceCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(PaymentErrors.InvoiceNotFound), StatusCodes.Status404NotFound);

        repository.Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}
