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
///     REST controller for guests.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Guests endpoints")]
public class GuestsController(
    IGuestQueryService queryService,
    IGuestCommandService commandService,
    IStringLocalizer<GuestStayMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<GuestStayMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // Guests are created during reservation or check-in flows; direct edition is not exposed by the current UI.

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all guests",
        Description = "Get all guests",
        OperationId = "GetAllGuests")]
    [SwaggerResponse(StatusCodes.Status200OK, "The guests were found", typeof(IEnumerable<GuestResource>))]
    public async Task<IActionResult> GetAllGuests(CancellationToken cancellationToken)
    {
        var query = new GetAllGuestsQuery();
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(GuestResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a guest",
        Description = "Create a guest",
        OperationId = "CreateGuest")]
    [SwaggerResponse(StatusCodes.Status201Created, "The guest was created", typeof(GuestResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The guest was not created")]
    public async Task<IActionResult> CreateGuest(
        [FromBody] CreateGuestResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateGuestCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return GuestActionResultAssembler.ToActionResultFromCreateGuestResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdEntity => StatusCode(
                StatusCodes.Status201Created,
                GuestResourceFromEntityAssembler.ToResourceFromEntity(createdEntity))
        );
    }



}
