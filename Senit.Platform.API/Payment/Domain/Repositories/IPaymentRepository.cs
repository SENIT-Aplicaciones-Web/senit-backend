using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.Payment.Domain.Repositories;

/// <summary>
///     Repository contract for payment entities.
/// </summary>
public interface IPaymentRepository : IBaseRepository<PaymentRecord>
{
    Task<IEnumerable<PaymentRecord>> ListByHotelIdAsync(string hotelId, CancellationToken cancellationToken = default);

}
