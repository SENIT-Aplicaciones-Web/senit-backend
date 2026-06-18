using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.Room.Application.CommandServices;
using Senit.Platform.API.Room.Application.QueryServices;
using Senit.Platform.API.Room.Domain.Model.Commands;
using Senit.Platform.API.Room.Domain.Model.Queries;
using Senit.Platform.API.Room.Interfaces.Rest.Resources;
using Senit.Platform.API.Room.Interfaces.Rest.Transform;
using Senit.Platform.API.Room.Resources;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.Room.Interfaces.Rest;

/// <summary>
///     REST controller for rooms.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Rooms endpoints")]
public class RoomsController(
    IRoomQueryService queryService,
    IRoomCommandService commandService,
    IStringLocalizer<RoomMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<RoomMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all rooms",
        Description = "Get all rooms",
        OperationId = "GetAllRooms")]
    [SwaggerResponse(StatusCodes.Status200OK, "The rooms were found", typeof(IEnumerable<RoomResource>))]
    public async Task<IActionResult> GetAllRooms(CancellationToken cancellationToken)
    {
        var query = new GetAllRoomsQuery();
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a room",
        Description = "Create a room",
        OperationId = "CreateRoom")]
    [SwaggerResponse(StatusCodes.Status201Created, "The room was created", typeof(RoomResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The room was not created")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The room conflicts with existing data")]
    public async Task<IActionResult> CreateRoom(
        [FromBody] CreateRoomResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateRoomCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return RoomActionResultAssembler.ToActionResultFromCreateRoomResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdEntity => StatusCode(
                StatusCodes.Status201Created,
                RoomResourceFromEntityAssembler.ToResourceFromEntity(createdEntity))
        );
    }

    [HttpPut("{roomId}")]
    [SwaggerOperation(
        Summary = "Update a room",
        Description = "Update a room",
        OperationId = "UpdateRoom")]
    [SwaggerResponse(StatusCodes.Status200OK, "The room was updated", typeof(RoomResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The room was not updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The room was not found")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The room conflicts with existing data")]
    public async Task<IActionResult> UpdateRoom(
        [FromRoute] string roomId,
        [FromBody] UpdateRoomResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateRoomCommandFromResourceAssembler.ToCommandFromResource(roomId, resource);
        var result = await commandService.Handle(command, cancellationToken);

        return RoomActionResultAssembler.ToActionResultFromUpdateRoomResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            updatedEntity => Ok(RoomResourceFromEntityAssembler.ToResourceFromEntity(updatedEntity))
        );
    }

    [HttpDelete("{roomId}")]
    [SwaggerOperation(
        Summary = "Delete a room",
        Description = "Delete a room",
        OperationId = "DeleteRoom")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The room was deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The room was not found")]
    public async Task<IActionResult> DeleteRoom([FromRoute] string roomId, CancellationToken cancellationToken)
    {
        var command = new DeleteRoomCommand(roomId);
        var result = await commandService.Handle(command, cancellationToken);

        return RoomActionResultAssembler.ToActionResultFromDeleteRoomResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            () => NoContent()
        );
    }
}
