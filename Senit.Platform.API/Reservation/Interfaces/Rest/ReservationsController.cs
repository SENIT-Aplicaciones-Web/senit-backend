using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.Reservation.Application.CommandServices;
using Senit.Platform.API.Reservation.Application.QueryServices;
using Senit.Platform.API.Reservation.Domain.Model.Commands;
using Senit.Platform.API.Reservation.Domain.Model.Queries;
using Senit.Platform.API.Reservation.Interfaces.Rest.Resources;
using Senit.Platform.API.Reservation.Interfaces.Rest.Transform;
using Senit.Platform.API.Reservation.Resources;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.Reservation.Interfaces.Rest;

/// <summary>
///     REST controller for reservations.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Reservations endpoints")]
public class ReservationsController(
    IReservationQueryService queryService,
    IReservationCommandService commandService,
    IStringLocalizer<ReservationMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ReservationMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // Reservations can be created or cancelled through updates, deletion is not exposed by the current UI.

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all reservations",
        Description = "Get reservations. When hotelId is provided, only reservations belonging to the requested hotel are returned.",
        OperationId = "GetAllReservations")]
    [SwaggerResponse(StatusCodes.Status200OK, "The reservations were found", typeof(IEnumerable<ReservationResource>))]
    public async Task<IActionResult> GetAllReservations(
        [SwaggerParameter("Hotel identifier used to return only reservations owned by the active hotel.", Required = false)]
        [FromQuery] string? hotelId,
        CancellationToken cancellationToken)
    {
        var query = new GetAllReservationsQuery(hotelId);
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a reservation",
        Description = "Create a reservation",
        OperationId = "CreateReservation")]
    [SwaggerResponse(StatusCodes.Status201Created, "The reservation was created", typeof(ReservationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The reservation was not created")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The reservation conflicts with an existing reservation or active stay")]
    public async Task<IActionResult> CreateReservation(
        [FromBody] CreateReservationResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateReservationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return ReservationActionResultAssembler.ToActionResultFromCreateReservationResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdEntity => StatusCode(
                StatusCodes.Status201Created,
                ReservationResourceFromEntityAssembler.ToResourceFromEntity(createdEntity))
        );
    }

    [HttpPut("{reservationId}")]
    [SwaggerOperation(
        Summary = "Update a reservation",
        Description = "Update a reservation",
        OperationId = "UpdateReservation")]
    [SwaggerResponse(StatusCodes.Status200OK, "The reservation was updated", typeof(ReservationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The reservation was not updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The reservation was not found")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The reservation conflicts with an existing reservation or active stay")]
    public async Task<IActionResult> UpdateReservation(
        [FromRoute] string reservationId,
        [FromBody] UpdateReservationResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateReservationCommandFromResourceAssembler.ToCommandFromResource(reservationId, resource);
        var result = await commandService.Handle(command, cancellationToken);

        return ReservationActionResultAssembler.ToActionResultFromUpdateReservationResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            updatedEntity => Ok(ReservationResourceFromEntityAssembler.ToResourceFromEntity(updatedEntity))
        );
    }


}
