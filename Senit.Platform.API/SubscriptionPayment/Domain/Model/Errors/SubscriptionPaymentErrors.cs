namespace Senit.Platform.API.SubscriptionPayment.Domain.Model.Errors;

/// <summary>
///     SubscriptionPayment context error codes used by application services.
/// </summary>
public enum SubscriptionPaymentErrors
{
    SubscriptionNotFound,
    SubscriptionPaymentNotFound,
    InvalidAmount,
    InvalidPlan,
    InvalidCheckoutRegistration,
    CheckoutSessionCouldNotBeCreated,
    CheckoutSessionNotFound,
    CheckoutHotelCouldNotBeActivated,
    CheckoutUserCouldNotBeActivated
}
