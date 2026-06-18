using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Senit.Platform.API.SubscriptionPayment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository for subscriptionpayment entities.
/// </summary>
public class SubscriptionPaymentRepository(AppDbContext context) : BaseRepository<SubscriptionPaymentRecord>(context), ISubscriptionPaymentRepository
{

}
