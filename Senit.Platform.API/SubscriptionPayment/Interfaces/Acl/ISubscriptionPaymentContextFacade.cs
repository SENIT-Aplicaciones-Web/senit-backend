namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Acl;

/// <summary>
///     Anti corruption facade exposed by the SubscriptionPayment bounded context.
/// </summary>
public interface ISubscriptionPaymentContextFacade
{
    /// <summary>
    ///     Creates a subscription and returns its identifier when successful.
    /// </summary>
    Task<string> CreateSubscription(
        string hotelId,
        string plan,
        string status,
        decimal monthlyAmount,
        DateTime startedAt,
        DateTime? endsAt,
        CancellationToken cancellationToken = default);
}
