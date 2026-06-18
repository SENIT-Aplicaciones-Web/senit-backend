using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.Payment.Domain.Repositories;

/// <summary>
///     Repository contract for invoice entities.
/// </summary>
public interface IInvoiceRepository : IBaseRepository<Invoice>
{

}
