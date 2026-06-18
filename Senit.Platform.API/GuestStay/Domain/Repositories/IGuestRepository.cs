using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.GuestStay.Domain.Repositories;

/// <summary>
///     Repository contract for guest entities.
/// </summary>
public interface IGuestRepository : IBaseRepository<Guest>
{
    Task<IEnumerable<Guest>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default);

}
