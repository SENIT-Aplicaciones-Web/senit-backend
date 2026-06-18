using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.Payment.Application.CommandServices;

/// <summary>
///     Command service contract for payment use cases.
/// </summary>
public interface IPaymentCommandService
{
    Task<ApplicationResult<PaymentRecord>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<PaymentRecord>> Handle(UpdatePaymentCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeletePaymentCommand command, CancellationToken cancellationToken = default);
}
