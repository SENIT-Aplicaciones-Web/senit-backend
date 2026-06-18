using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Domain.Model.Queries;

namespace Senit.Platform.API.Payment.Application.QueryServices;

/// <summary>
///     Query service contract for payment use cases.
/// </summary>
public interface IPaymentQueryService
{
    Task<IEnumerable<PaymentRecord>> Handle(GetAllPaymentsQuery query, CancellationToken cancellationToken = default);

    Task<PaymentRecord?> Handle(GetPaymentByIdQuery query, CancellationToken cancellationToken = default);
}
