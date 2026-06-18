using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.SubscriptionPayment.Application.CommandServices;
using Senit.Platform.API.SubscriptionPayment.Application.QueryServices;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Queries;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;
using Senit.Platform.API.SubscriptionPayment.Resources;
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
    IStringLocalizer<SubscriptionPaymentMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<SubscriptionPaymentMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // Subscription payments are shown as a history list in the frontend.
    // The current user interface does not open an individual subscription payment detail page.

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
