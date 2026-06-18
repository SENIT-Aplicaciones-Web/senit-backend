using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.SubscriptionPayment.Application.CommandServices;

/// <summary>
///     Command service contract for subscriptionpayment use cases.
/// </summary>
public interface ISubscriptionPaymentCommandService
{
    Task<ApplicationResult<SubscriptionPaymentRecord>> Handle(CreateSubscriptionPaymentCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<SubscriptionPaymentRecord>> Handle(UpdateSubscriptionPaymentCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteSubscriptionPaymentCommand command, CancellationToken cancellationToken = default);
}
