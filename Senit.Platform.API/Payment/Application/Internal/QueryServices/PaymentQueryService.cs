using Senit.Platform.API.Payment.Application.QueryServices;
using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Domain.Model.Queries;
using Senit.Platform.API.Payment.Domain.Repositories;

namespace Senit.Platform.API.Payment.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for payment use cases.
/// </summary>
public class PaymentQueryService(IPaymentRepository repository) : IPaymentQueryService
{
    public async Task<IEnumerable<PaymentRecord>> Handle(GetAllPaymentsQuery query, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(query.HotelId))
            return await repository.ListByHotelIdAsync(query.HotelId, cancellationToken);

        return await repository.ListAsync(cancellationToken);
    }

    public async Task<PaymentRecord?> Handle(GetPaymentByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(query.Id, cancellationToken);
    }
}
