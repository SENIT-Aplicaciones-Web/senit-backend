using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.SubscriptionPayment.Domain.Repositories;

/// <summary>
///     Repository contract for subscriptionpayment entities.
/// </summary>
public interface ISubscriptionPaymentRepository : IBaseRepository<SubscriptionPaymentRecord>
{

}
