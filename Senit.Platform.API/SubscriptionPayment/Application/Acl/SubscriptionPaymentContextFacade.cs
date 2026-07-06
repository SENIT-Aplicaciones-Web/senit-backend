using Senit.Platform.API.SubscriptionPayment.Application.CommandServices;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Acl;

namespace Senit.Platform.API.SubscriptionPayment.Application.Acl;

/// <summary>
///     Anti corruption facade for the SubscriptionPayment bounded context.
/// </summary>
public class SubscriptionPaymentContextFacade(
    ISubscriptionCommandService subscriptionCommandService) : ISubscriptionPaymentContextFacade
{
    /// <summary>
    ///     Creates a subscription through the SubscriptionPayment bounded context.
    /// </summary>
    public async Task<string> CreateSubscription(
        string hotelId,
        string plan,
        string status,
        decimal monthlyAmount,
        DateTime startedAt,
        DateTime? endsAt,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateSubscriptionCommand(hotelId, plan, status, monthlyAmount, startedAt, endsAt);
        var result = await subscriptionCommandService.Handle(command, cancellationToken);
        return result.IsSuccess ? result.Value?.Id ?? string.Empty : string.Empty;
    }
}
