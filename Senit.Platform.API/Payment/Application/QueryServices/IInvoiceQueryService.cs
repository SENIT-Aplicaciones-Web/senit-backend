using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Domain.Model.Queries;

namespace Senit.Platform.API.Payment.Application.QueryServices;

/// <summary>
///     Query service contract for invoice use cases.
/// </summary>
public interface IInvoiceQueryService
{
    Task<IEnumerable<Invoice>> Handle(GetAllInvoicesQuery query, CancellationToken cancellationToken = default);

    Task<Invoice?> Handle(GetInvoiceByIdQuery query, CancellationToken cancellationToken = default);
}
