using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Senit.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.SubscriptionPayment.Application.CommandServices;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.SubscriptionPayment.Application.QueryServices;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Queries;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;
using Senit.Platform.API.SubscriptionPayment.Resources;
using Senit.Platform.API.Shared.Interfaces.Rest.Transform;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest;

/// <summary>
///     REST controller for subscription payment history.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available SubscriptionPayments endpoints")]
public class SubscriptionPaymentsController(
    ISubscriptionPaymentQueryService queryService,
    ISubscriptionPaymentCommandService commandService,
    IStripeSubscriptionCheckoutCommandService checkoutCommandService,
    IStringLocalizer<SubscriptionPaymentMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<SubscriptionPaymentMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // Subscription payments are shown as a history list in the frontend.
    // The current user interface does not open an individual subscription payment detail page.


    [HttpPost("stripe-checkout/sessions")]
    [SwaggerOperation(
        Summary = "Create a Stripe Checkout session",
        Description = "Create a Stripe hosted Checkout session for a new hotel subscription registration",
        OperationId = "CreateStripeSubscriptionCheckoutSession")]
    [SwaggerResponse(StatusCodes.Status201Created, "The Stripe Checkout session was created", typeof(StripeCheckoutSessionResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The checkout registration data is invalid")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The email already has an active hotel assignment")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateStripeCheckoutSession(
        [FromBody] CreateStripeCheckoutSessionResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateStripeCheckoutSessionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await checkoutCommandService.Handle(command, cancellationToken);

        return ActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            session => StatusCode(
                StatusCodes.Status201Created,
                StripeCheckoutSessionResourceFromResultAssembler.ToResourceFromResult(session)));
    }

    [HttpGet("stripe-checkout/sessions/{sessionId}")]
    [SwaggerOperation(
        Summary = "Get a Stripe Checkout session",
        Description = "Get the current status of a Stripe Checkout session and activate the hotel registration when Stripe confirms payment",
        OperationId = "GetStripeSubscriptionCheckoutSession")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Stripe Checkout session was found", typeof(StripeCheckoutSessionResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Stripe Checkout session was not found")]
    [AllowAnonymous]
    public async Task<IActionResult> GetStripeCheckoutSession(
        [FromRoute] string sessionId,
        CancellationToken cancellationToken)
    {
        var result = await checkoutCommandService.GetSession(sessionId, cancellationToken);

        return ActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            session => Ok(StripeCheckoutSessionResourceFromResultAssembler.ToResourceFromResult(session)));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all subscription payments",
        Description = "Get subscription payments. When hotelId is provided, only subscription payments belonging to the requested hotel are returned.",
        OperationId = "GetAllSubscriptionPayments")]
    [SwaggerResponse(StatusCodes.Status200OK, "The subscription payments were found", typeof(IEnumerable<SubscriptionPaymentResource>))]
    public async Task<IActionResult> GetAllSubscriptionPayments(
        [SwaggerParameter("Hotel identifier used to return only subscription payments owned by the active hotel.", Required = false)]
        [FromQuery] string? hotelId,
        CancellationToken cancellationToken)
    {
        var query = new GetAllSubscriptionPaymentsQuery(hotelId);
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(SubscriptionPaymentResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a subscription payment",
        Description = "Create a subscription payment",
        OperationId = "CreateSubscriptionPayment")]
    [SwaggerResponse(StatusCodes.Status201Created, "The subscription payment was created", typeof(SubscriptionPaymentResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The subscription payment was not created")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The subscription was not found")]
    public async Task<IActionResult> CreateSubscriptionPayment(
        [FromBody] CreateSubscriptionPaymentResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateSubscriptionPaymentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return SubscriptionPaymentActionResultAssembler.ToActionResultFromCreateSubscriptionPaymentResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdEntity => StatusCode(
                StatusCodes.Status201Created,
                SubscriptionPaymentResourceFromEntityAssembler.ToResourceFromEntity(createdEntity))
        );
    }
}
