using Senit.Platform.API.FrontDesk.Interfaces.Acl;
using Senit.Platform.API.Iam.Domain.Model.ValueObjects;
using Senit.Platform.API.Iam.Interfaces.Acl;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.SubscriptionPayment.Application.CommandServices;
using Senit.Platform.API.SubscriptionPayment.Application.External.PaymentGateway;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Errors;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.ValueObjects;
using Senit.Platform.API.SubscriptionPayment.Domain.Repositories;

namespace Senit.Platform.API.SubscriptionPayment.Application.Internal.CommandServices;

/// <summary>
///     Coordinates Stripe hosted Checkout inside the SubscriptionPayment bounded context.
/// </summary>
public class StripeSubscriptionCheckoutCommandService(
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionPaymentRepository subscriptionPaymentRepository,
    IFrontDeskContextFacade frontDeskContextFacade,
    IIamContextFacade iamContextFacade,
    IStripeSubscriptionPaymentGateway paymentGateway,
    IStripeCheckoutSessionStore checkoutSessionStore,
    IUnitOfWork unitOfWork) : IStripeSubscriptionCheckoutCommandService
{
    public async Task<ApplicationResult<StripeCheckoutSessionResult>> Handle(
        CreateStripeSubscriptionCheckoutSessionCommand command,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = EmailAddress.Normalize(command.Email);
        var username = command.Username.Trim();
        var plan = SubscriptionPlanCatalog.Normalize(command.Plan);
        var amount = SubscriptionPlanCatalog.GetMonthlyAmount(plan);

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(normalizedEmail) ||
            string.IsNullOrWhiteSpace(command.Password))
            return ApplicationResult<StripeCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.InvalidCheckoutRegistration),
                StatusCodes.Status400BadRequest);

        if (!SubscriptionPlanCatalog.IsSupported(plan))
            return ApplicationResult<StripeCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.InvalidPlan),
                StatusCodes.Status400BadRequest);

        if (!await iamContextFacade.CanRegisterHotelAdministrator(normalizedEmail, cancellationToken))
            return ApplicationResult<StripeCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.CheckoutSessionCouldNotBeCreated),
                StatusCodes.Status409Conflict);

        var result = await paymentGateway.CreateSubscriptionCheckoutSessionAsync(
            plan,
            amount,
            normalizedEmail,
            cancellationToken);

        if (result is null || string.IsNullOrWhiteSpace(result.Id) || string.IsNullOrWhiteSpace(result.CheckoutUrl))
            return ApplicationResult<StripeCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.CheckoutSessionCouldNotBeCreated),
                StatusCodes.Status400BadRequest);

        checkoutSessionStore.Save(new StripeCheckoutRegistrationSession(
            result.Id,
            username,
            normalizedEmail,
            command.Password,
            plan,
            amount));

        return ApplicationResult<StripeCheckoutSessionResult>.Created(result);
    }

    public async Task<ApplicationResult<StripeCheckoutSessionResult>> GetSession(
        string sessionId,
        CancellationToken cancellationToken = default)
    {
        var stripeSession = await paymentGateway.RetrieveCheckoutSessionAsync(sessionId, cancellationToken);

        if (stripeSession is null)
            return ApplicationResult<StripeCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.CheckoutSessionNotFound),
                StatusCodes.Status404NotFound);

        if (!checkoutSessionStore.TryGet(sessionId, out var registrationSession))
        {
            return stripeSession.Status == "completed"
                ? ApplicationResult<StripeCheckoutSessionResult>.Failure(
                    nameof(SubscriptionPaymentErrors.CheckoutRegistrationExpired),
                    StatusCodes.Status409Conflict)
                : ApplicationResult<StripeCheckoutSessionResult>.Success(stripeSession);
        }

        if (registrationSession.Status == "completed")
            return ApplicationResult<StripeCheckoutSessionResult>.Success(stripeSession with
            {
                Status = "completed",
                PaymentStatus = "paid"
            });

        if (stripeSession.Status == "completed" && stripeSession.PaymentStatus == "paid")
        {
            var activationResult = await ActivateRegistration(registrationSession, cancellationToken);
            if (!activationResult.IsSuccess) return activationResult;

            return ApplicationResult<StripeCheckoutSessionResult>.Success(stripeSession with
            {
                Status = "completed",
                PaymentStatus = "paid"
            });
        }

        return ApplicationResult<StripeCheckoutSessionResult>.Success(stripeSession);
    }

    private async Task<ApplicationResult<StripeCheckoutSessionResult>> ActivateRegistration(
        StripeCheckoutRegistrationSession session,
        CancellationToken cancellationToken)
    {
        if (!await iamContextFacade.CanRegisterHotelAdministrator(session.Email, cancellationToken))
            return ApplicationResult<StripeCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.CheckoutSessionCouldNotBeCreated),
                StatusCodes.Status409Conflict);

        var plan = SubscriptionPlanCatalog.Normalize(session.Plan);
        var amount = SubscriptionPlanCatalog.GetMonthlyAmount(plan);
        var paidAt = DateTime.UtcNow;

        var hotelId = await frontDeskContextFacade.CreateHotel(
            $"Hotel de {session.Username}",
            string.Empty,
            string.Empty,
            string.Empty,
            session.Email,
            plan,
            "active",
            cancellationToken);

        if (string.IsNullOrWhiteSpace(hotelId))
            return ApplicationResult<StripeCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.CheckoutHotelCouldNotBeActivated),
                StatusCodes.Status400BadRequest);

        var userId = await iamContextFacade.CreateUser(
            hotelId,
            session.Username,
            session.Username,
            session.Email,
            session.Password,
            "ADMIN",
            "active",
            cancellationToken);

        if (string.IsNullOrWhiteSpace(userId))
            return ApplicationResult<StripeCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.CheckoutUserCouldNotBeActivated),
                StatusCodes.Status400BadRequest);

        var subscription = new Subscription(
            session.Id,
            hotelId,
            plan,
            "active",
            amount,
            paidAt,
            null);

        await subscriptionRepository.AddAsync(subscription, cancellationToken);

        var payment = new SubscriptionPaymentRecord(
            Guid.NewGuid().ToString(),
            subscription.Id,
            hotelId,
            plan,
            amount,
            "stripe-test",
            "completed",
            paidAt);

        await subscriptionPaymentRepository.AddAsync(payment, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        session.MarkCompleted(hotelId, subscription.Id, paidAt);

        return ApplicationResult<StripeCheckoutSessionResult>.Success(new StripeCheckoutSessionResult(
            session.Id,
            string.Empty,
            string.Empty,
            string.Empty,
            plan,
            amount,
            "PEN",
            "completed",
            "paid",
            session.Email));
    }
}
