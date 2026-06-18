using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.SubscriptionPayment.Domain.Repositories;

/// <summary>
///     Repository contract for subscription entities.
/// </summary>
public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    Task<IEnumerable<Subscription>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default);

}
