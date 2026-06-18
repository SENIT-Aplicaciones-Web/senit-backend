using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.GuestStay.Application.CommandServices;
using Senit.Platform.API.GuestStay.Application.QueryServices;
using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.GuestStay.Domain.Model.Queries;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;
using Senit.Platform.API.GuestStay.Resources;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest;

/// <summary>
///     REST controller for consumptions.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Consumptions endpoints")]
public class ConsumptionsController(
    IConsumptionQueryService queryService,
    IConsumptionCommandService commandService,
    IStringLocalizer<GuestStayMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<GuestStayMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all consumptions",
        Description = "Get all consumptions",
        OperationId = "GetAllConsumptions")]
    [SwaggerResponse(StatusCodes.Status200OK, "The consumptions were found", typeof(IEnumerable<ConsumptionResource>))]
    public async Task<IActionResult> GetAllConsumptions(CancellationToken cancellationToken)
    {
        var query = new GetAllConsumptionsQuery();
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(ConsumptionResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a consumption",
        Description = "Create a consumption",
        OperationId = "CreateConsumption")]
    [SwaggerResponse(StatusCodes.Status201Created, "The consumption was created", typeof(ConsumptionResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The consumption was not created")]
    public async Task<IActionResult> CreateConsumption(
        [FromBody] CreateConsumptionResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateConsumptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return ConsumptionActionResultAssembler.ToActionResultFromCreateConsumptionResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdEntity => StatusCode(
                StatusCodes.Status201Created,
                ConsumptionResourceFromEntityAssembler.ToResourceFromEntity(createdEntity))
        );
    }

    [HttpPut("{consumptionId}")]
    [SwaggerOperation(
        Summary = "Update a consumption",
        Description = "Update a consumption",
        OperationId = "UpdateConsumption")]
    [SwaggerResponse(StatusCodes.Status200OK, "The consumption was updated", typeof(ConsumptionResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The consumption was not found")]
    public async Task<IActionResult> UpdateConsumption(
        [FromRoute] string consumptionId,
        [FromBody] UpdateConsumptionResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateConsumptionCommandFromResourceAssembler.ToCommandFromResource(consumptionId, resource);
        var result = await commandService.Handle(command, cancellationToken);

        return ConsumptionActionResultAssembler.ToActionResultFromUpdateConsumptionResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            updatedEntity => Ok(ConsumptionResourceFromEntityAssembler.ToResourceFromEntity(updatedEntity))
        );
    }

    [HttpDelete("{consumptionId}")]
    [SwaggerOperation(
        Summary = "Delete a consumption",
        Description = "Delete a consumption",
        OperationId = "DeleteConsumption")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The consumption was deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The consumption was not found")]
    public async Task<IActionResult> DeleteConsumption([FromRoute] string consumptionId, CancellationToken cancellationToken)
    {
        var command = new DeleteConsumptionCommand(consumptionId);
        var result = await commandService.Handle(command, cancellationToken);

        return ConsumptionActionResultAssembler.ToActionResultFromDeleteConsumptionResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            () => NoContent()
        );
    }
}
