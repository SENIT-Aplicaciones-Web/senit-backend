using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.Reservation.Domain.Model.Aggregates;
using Senit.Platform.API.Reservation.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.Reservation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for reservation entities.
/// </summary>
public class ReservationRepository(AppDbContext context) : BaseRepository<HotelReservation>(context), IReservationRepository
{
    public async Task<IEnumerable<HotelReservation>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<HotelReservation>()
            .Where(reservation => reservation.HotelId == hotelId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsOverlappingReservationAsync(string roomId, DateTime startAt, DateTime endAt, string? excludedId = null, CancellationToken cancellationToken = default)
    {
        return await Context.Set<HotelReservation>()
            .AnyAsync(reservation =>
                reservation.RoomId == roomId &&
                reservation.Status != "cancelled" &&
                (excludedId == null || reservation.Id != excludedId) &&
                startAt < reservation.EndAt &&
                endAt > reservation.StartAt,
                cancellationToken);
    }
}
