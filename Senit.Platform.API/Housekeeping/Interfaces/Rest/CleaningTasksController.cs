using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.Housekeeping.Application.CommandServices;
using Senit.Platform.API.Housekeeping.Application.QueryServices;
using Senit.Platform.API.Housekeeping.Domain.Model.Queries;
using Senit.Platform.API.Housekeeping.Interfaces.Rest.Resources;
using Senit.Platform.API.Housekeeping.Interfaces.Rest.Transform;
using Senit.Platform.API.Housekeeping.Resources;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.Housekeeping.Interfaces.Rest;

/// <summary>
///     REST controller for cleaning tasks.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available CleaningTasks endpoints")]
public class CleaningTasksController(
    ICleaningTaskQueryService queryService,
    ICleaningTaskCommandService commandService,
    IStringLocalizer<HousekeepingMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<HousekeepingMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // Cleaning tasks are created by checkout workflows and completed through room status changes.

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all cleaning tasks",
        Description = "Get cleaning tasks. When hotelId is provided, only cleaning tasks belonging to the requested hotel are returned.",
        OperationId = "GetAllCleaningTasks")]
    [SwaggerResponse(StatusCodes.Status200OK, "The cleaning tasks were found", typeof(IEnumerable<CleaningTaskResource>))]
    public async Task<IActionResult> GetAllCleaningTasks(
        [SwaggerParameter("Hotel identifier used to return only cleaning tasks owned by the active hotel.", Required = false)]
        [FromQuery] string? hotelId,
        CancellationToken cancellationToken)
    {
        var query = new GetAllCleaningTasksQuery(hotelId);
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(CleaningTaskResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a cleaning task",
        Description = "Create a cleaning task",
        OperationId = "CreateCleaningTask")]
    [SwaggerResponse(StatusCodes.Status201Created, "The cleaning task was created", typeof(CleaningTaskResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The cleaning task was not created")]
    public async Task<IActionResult> CreateCleaningTask(
        [FromBody] CreateCleaningTaskResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateCleaningTaskCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return CleaningTaskActionResultAssembler.ToActionResultFromCreateCleaningTaskResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdEntity => StatusCode(
                StatusCodes.Status201Created,
                CleaningTaskResourceFromEntityAssembler.ToResourceFromEntity(createdEntity))
        );
    }
}
