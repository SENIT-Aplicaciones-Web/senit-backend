using Senit.Platform.API.Iam.Domain.Model.ValueObjects;
using Senit.Platform.API.FrontDesk.Interfaces.Acl;
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
///     Coordinates the simulated Stripe checkout flow inside the SubscriptionPayment bounded context.
/// </summary>
public class SimulatedSubscriptionCheckoutCommandService(
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionPaymentRepository subscriptionPaymentRepository,
    IFrontDeskContextFacade frontDeskContextFacade,
    IIamContextFacade iamContextFacade,
    ISimulatedSubscriptionPaymentGateway paymentGateway,
    ISimulatedCheckoutSessionStore checkoutSessionStore,
    IUnitOfWork unitOfWork) : ISimulatedSubscriptionCheckoutCommandService
{
    public async Task<ApplicationResult<SimulatedCheckoutSessionResult>> Handle(
        CreateSimulatedSubscriptionCheckoutSessionCommand command,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = EmailAddress.Normalize(command.Email);
        var username = command.Username.Trim();
        var plan = SubscriptionPlanCatalog.Normalize(command.Plan);
        var amount = SubscriptionPlanCatalog.GetMonthlyAmount(plan);

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(normalizedEmail) ||
            string.IsNullOrWhiteSpace(command.Password))
            return ApplicationResult<SimulatedCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.InvalidCheckoutRegistration),
                StatusCodes.Status400BadRequest);

        if (!SubscriptionPlanCatalog.IsSupported(plan))
            return ApplicationResult<SimulatedCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.InvalidPlan),
                StatusCodes.Status400BadRequest);

        if (!await iamContextFacade.CanRegisterHotelAdministrator(normalizedEmail, cancellationToken))
            return ApplicationResult<SimulatedCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.CheckoutSessionCouldNotBeCreated),
                StatusCodes.Status409Conflict);

        var sessionId = $"cs_test_{Guid.NewGuid():N}";
        var session = new SimulatedCheckoutRegistrationSession(
            sessionId,
            username,
            normalizedEmail,
            command.Password,
            plan,
            amount);

        checkoutSessionStore.Save(session);

        return ApplicationResult<SimulatedCheckoutSessionResult>.Created(
            BuildResult(session));
    }

    public async Task<ApplicationResult<SimulatedCheckoutSessionResult>> Handle(
        CompleteSimulatedSubscriptionCheckoutSessionCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!checkoutSessionStore.TryGet(command.SessionId, out var session))
            return ApplicationResult<SimulatedCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.CheckoutSessionNotFound),
                StatusCodes.Status404NotFound);

        if (session.Status == "completed")
            return ApplicationResult<SimulatedCheckoutSessionResult>.Success(BuildResult(session));

        if (!await iamContextFacade.CanRegisterHotelAdministrator(session.Email, cancellationToken))
            return ApplicationResult<SimulatedCheckoutSessionResult>.Failure(
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
            return ApplicationResult<SimulatedCheckoutSessionResult>.Failure(
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
            return ApplicationResult<SimulatedCheckoutSessionResult>.Failure(
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
            "simulated-stripe",
            "completed",
            paidAt);

        await subscriptionPaymentRepository.AddAsync(payment, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        session.MarkCompleted(hotelId, subscription.Id, paidAt);

        return ApplicationResult<SimulatedCheckoutSessionResult>.Success(BuildResult(session));
    }

    public Task<ApplicationResult<SimulatedCheckoutSessionResult>> GetSession(
        string sessionId,
        CancellationToken cancellationToken = default)
    {
        if (!checkoutSessionStore.TryGet(sessionId, out var session))
            return Task.FromResult(ApplicationResult<SimulatedCheckoutSessionResult>.Failure(
                nameof(SubscriptionPaymentErrors.CheckoutSessionNotFound),
                StatusCodes.Status404NotFound));

        return Task.FromResult(ApplicationResult<SimulatedCheckoutSessionResult>.Success(BuildResult(session)));
    }

    private SimulatedCheckoutSessionResult BuildResult(SimulatedCheckoutRegistrationSession session)
    {
        return paymentGateway.CreateSession(
            session.Id,
            session.Plan,
            session.Amount,
            session.Email) with
        {
            Status = session.Status,
            SuccessUrl = paymentGateway.BuildSuccessUrl(session.Id)
        };
    }
}
