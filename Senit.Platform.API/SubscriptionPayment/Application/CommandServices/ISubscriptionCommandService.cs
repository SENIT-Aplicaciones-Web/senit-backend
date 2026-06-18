using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.SubscriptionPayment.Application.CommandServices;

/// <summary>
///     Command service contract for subscription use cases.
/// </summary>
public interface ISubscriptionCommandService
{
    Task<ApplicationResult<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<Subscription>> Handle(UpdateSubscriptionCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteSubscriptionCommand command, CancellationToken cancellationToken = default);
}
