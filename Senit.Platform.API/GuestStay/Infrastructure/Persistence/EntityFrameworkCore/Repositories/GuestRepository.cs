using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.GuestStay.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for guest entities.
/// </summary>
public class GuestRepository(AppDbContext context) : BaseRepository<Guest>(context), IGuestRepository
{
    public async Task<IEnumerable<Guest>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default)
    {
        var query =
            from guest in Context.Set<Guest>()
            join stay in Context.Set<GuestStayRecord>() on guest.Id equals stay.GuestId
            where stay.HotelId == hotelId
            select guest;

        return await query.Distinct().ToListAsync(cancellationToken);
    }
}
