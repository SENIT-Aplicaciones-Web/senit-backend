using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.Room.Domain.Repositories;

/// <summary>
///     Repository contract for room entities.
/// </summary>
public interface IRoomRepository : IBaseRepository<RoomEntity>
{
    Task<IEnumerable<RoomEntity>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByHotelIdAndNumberAsync(string hotelId, string number, string? excludedId = null, CancellationToken cancellationToken = default);
}
