using Senit.Platform.API.SubscriptionPayment.Application.QueryServices;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Queries;
using Senit.Platform.API.SubscriptionPayment.Domain.Repositories;

namespace Senit.Platform.API.SubscriptionPayment.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for subscription use cases.
/// </summary>
public class SubscriptionQueryService(ISubscriptionRepository repository) : ISubscriptionQueryService
{
    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(query.HotelId))
            return await repository.ListByHotelIdAsync(query.HotelId, cancellationToken);

        return await repository.ListAsync(cancellationToken);
    }

    public async Task<Subscription?> Handle(GetSubscriptionByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(query.Id, cancellationToken);
    }
}
