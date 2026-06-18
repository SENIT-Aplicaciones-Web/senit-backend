using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.Payment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for payment entities.
/// </summary>
public class PaymentRepository(AppDbContext context) : BaseRepository<PaymentRecord>(context), IPaymentRepository
{
    public async Task<IEnumerable<PaymentRecord>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<PaymentRecord>()
            .Where(payment => payment.HotelId == hotelId)
            .ToListAsync(cancellationToken);
    }
}
