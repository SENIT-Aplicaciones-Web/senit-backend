using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.SubscriptionPayment.Application.CommandServices;
using Senit.Platform.API.SubscriptionPayment.Application.QueryServices;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Commands;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Queries;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Resources;
using Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;
using Senit.Platform.API.SubscriptionPayment.Resources;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest;

/// <summary>
///     REST controller for subscriptions.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Subscriptions endpoints")]
public class SubscriptionsController(
    ISubscriptionQueryService queryService,
    ISubscriptionCommandService commandService,
    IStringLocalizer<SubscriptionPaymentMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<SubscriptionPaymentMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // Subscriptions are consulted through plan changes. Direct creation and deletion are not exposed by the current UI.

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all subscriptions",
        Description = "Get subscriptions. When hotelId is provided, only subscriptions belonging to the requested hotel are returned.",
        OperationId = "GetAllSubscriptions")]
    [SwaggerResponse(StatusCodes.Status200OK, "The subscriptions were found", typeof(IEnumerable<SubscriptionResource>))]
    public async Task<IActionResult> GetAllSubscriptions(
        [SwaggerParameter("Hotel identifier used to return only subscriptions owned by the active hotel.", Required = false)]
        [FromQuery] string? hotelId,
        CancellationToken cancellationToken)
    {
        var query = new GetAllSubscriptionsQuery(hotelId);
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPut("{subscriptionId}")]
    [SwaggerOperation(
        Summary = "Update a subscription",
        Description = "Update a subscription",
        OperationId = "UpdateSubscription")]
    [SwaggerResponse(StatusCodes.Status200OK, "The subscription was updated", typeof(SubscriptionResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The subscription was not found")]
    public async Task<IActionResult> UpdateSubscription(
        [FromRoute] string subscriptionId,
        [FromBody] UpdateSubscriptionResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(subscriptionId, resource);
        var result = await commandService.Handle(command, cancellationToken);

        return SubscriptionActionResultAssembler.ToActionResultFromUpdateSubscriptionResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            updatedEntity => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(updatedEntity))
        );
    }


}
