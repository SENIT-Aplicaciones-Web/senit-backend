using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.Payment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for invoice entities.
/// </summary>
public class InvoiceRepository(AppDbContext context) : BaseRepository<Invoice>(context), IInvoiceRepository
{
    public async Task<IEnumerable<Invoice>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default)
    {
        var query =
            from invoice in Context.Set<Invoice>()
            join payment in Context.Set<PaymentRecord>() on invoice.PaymentId equals payment.Id
            where payment.HotelId == hotelId
            select invoice;

        return await query.ToListAsync(cancellationToken);
    }
}
