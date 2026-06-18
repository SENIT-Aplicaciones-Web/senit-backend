using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.GuestStay.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for consumption entities.
/// </summary>
public class ConsumptionRepository(AppDbContext context) : BaseRepository<Consumption>(context), IConsumptionRepository
{
    public async Task<IEnumerable<Consumption>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default)
    {
        var query =
            from consumption in Context.Set<Consumption>()
            join stay in Context.Set<GuestStayRecord>() on consumption.GuestStayId equals stay.Id
            where stay.HotelId == hotelId
            select consumption;

        return await query.ToListAsync(cancellationToken);
    }
}
