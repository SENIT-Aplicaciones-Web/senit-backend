using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Queries;

namespace Senit.Platform.API.SubscriptionPayment.Application.QueryServices;

/// <summary>
///     Query service contract for subscriptionpayment use cases.
/// </summary>
public interface ISubscriptionPaymentQueryService
{
    Task<IEnumerable<SubscriptionPaymentRecord>> Handle(GetAllSubscriptionPaymentsQuery query, CancellationToken cancellationToken = default);

    Task<SubscriptionPaymentRecord?> Handle(GetSubscriptionPaymentByIdQuery query, CancellationToken cancellationToken = default);
}
