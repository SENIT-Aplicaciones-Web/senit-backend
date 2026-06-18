using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Queries;

namespace Senit.Platform.API.SubscriptionPayment.Application.QueryServices;

/// <summary>
///     Query service contract for subscription use cases.
/// </summary>
public interface ISubscriptionQueryService
{
    Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query, CancellationToken cancellationToken = default);

    Task<Subscription?> Handle(GetSubscriptionByIdQuery query, CancellationToken cancellationToken = default);
}
