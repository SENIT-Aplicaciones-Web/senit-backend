using Senit.Platform.API.Payment.Application.QueryServices;
using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Domain.Model.Queries;
using Senit.Platform.API.Payment.Domain.Repositories;

namespace Senit.Platform.API.Payment.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for invoice use cases.
/// </summary>
public class InvoiceQueryService(IInvoiceRepository repository) : IInvoiceQueryService
{
    public async Task<IEnumerable<Invoice>> Handle(GetAllInvoicesQuery query, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(query.HotelId))
            return await repository.ListByHotelIdAsync(query.HotelId, cancellationToken);

        return await repository.ListAsync(cancellationToken);
    }

    public async Task<Invoice?> Handle(GetInvoiceByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(query.Id, cancellationToken);
    }
}
