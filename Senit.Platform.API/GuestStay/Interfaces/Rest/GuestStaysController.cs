using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.GuestStay.Application.CommandServices;
using Senit.Platform.API.GuestStay.Application.QueryServices;
using Senit.Platform.API.GuestStay.Domain.Model.Queries;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Resources;
using Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;
using Senit.Platform.API.GuestStay.Resources;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest;

/// <summary>
///     REST controller for guest stays.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available GuestStays endpoints")]
public class GuestStaysController(
    IGuestStayQueryService queryService,
    IGuestStayCommandService commandService,
    IStringLocalizer<GuestStayMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<GuestStayMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // Guest stays are created through check-in. Checkout is completed by payment and invoice workflows.

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all guest stays",
        Description = "Get all guest stays",
        OperationId = "GetAllGuestStays")]
    [SwaggerResponse(StatusCodes.Status200OK, "The guest stays were found", typeof(IEnumerable<GuestStayResource>))]
    public async Task<IActionResult> GetAllGuestStays(CancellationToken cancellationToken)
    {
        var query = new GetAllGuestStaysQuery();
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(GuestStayResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a guest stay",
        Description = "Create a guest stay",
        OperationId = "CreateGuestStay")]
    [SwaggerResponse(StatusCodes.Status201Created, "The guest stay was created", typeof(GuestStayResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The guest stay was not created")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The guest stay conflicts with the current room availability")]
    public async Task<IActionResult> CreateGuestStay(
        [FromBody] CreateGuestStayResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateGuestStayCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return GuestStayActionResultAssembler.ToActionResultFromCreateGuestStayResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdEntity => StatusCode(
                StatusCodes.Status201Created,
                GuestStayResourceFromEntityAssembler.ToResourceFromEntity(createdEntity))
        );
    }
}
