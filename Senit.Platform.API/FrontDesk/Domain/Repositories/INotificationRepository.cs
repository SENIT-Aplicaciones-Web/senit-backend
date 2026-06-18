using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.FrontDesk.Domain.Repositories;

/// <summary>
///     Repository contract for notification entities.
/// </summary>
public interface INotificationRepository : IBaseRepository<Notification>
{
    Task<IEnumerable<Notification>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default);

}
