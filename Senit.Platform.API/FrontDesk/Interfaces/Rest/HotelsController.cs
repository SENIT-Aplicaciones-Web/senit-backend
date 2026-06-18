using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.Shared.Interfaces.Rest.Transform;
using Senit.Platform.API.Iam.Resources;
using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Iam.Application.CommandServices;
using Senit.Platform.API.FrontDesk.Application.CommandServices;
using Senit.Platform.API.FrontDesk.Application.QueryServices;
using Senit.Platform.API.FrontDesk.Domain.Model.Commands;
using Senit.Platform.API.FrontDesk.Domain.Model.Queries;
using Senit.Platform.API.FrontDesk.Interfaces.Rest.Resources;
using Senit.Platform.API.FrontDesk.Interfaces.Rest.Transform;
using Senit.Platform.API.FrontDesk.Resources;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.FrontDesk.Interfaces.Rest;

/// <summary>
///     REST controller for hotels.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Hotels endpoints")]
public class HotelsController(
    IHotelQueryService queryService,
    IHotelCommandService commandService,
    IUserCommandService userCommandService,
    IStringLocalizer<FrontDeskMessages> contextLocalizer,
    IStringLocalizer<IamMessages> iamLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IUserCommandService _userCommandService = userCommandService;
    private readonly IStringLocalizer<FrontDeskMessages> _contextLocalizer = contextLocalizer;
    private readonly IStringLocalizer<IamMessages> _iamLocalizer = iamLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // Hotel creation is handled by the sign-up use case to ensure every hotel has an administrator.

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all hotels",
        Description = "Get all hotels",
        OperationId = "GetAllHotels")]
    [SwaggerResponse(StatusCodes.Status200OK, "The hotels were found", typeof(IEnumerable<HotelResource>))]
    public async Task<IActionResult> GetAllHotels(CancellationToken cancellationToken)
    {
        var query = new GetAllHotelsQuery();
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(HotelResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPut("{hotelId}")]
    [SwaggerOperation(
        Summary = "Update a hotel",
        Description = "Update a hotel",
        OperationId = "UpdateHotel")]
    [SwaggerResponse(StatusCodes.Status200OK, "The hotel was updated", typeof(HotelResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The hotel was not found")]
    public async Task<IActionResult> UpdateHotel(
        [FromRoute] string hotelId,
        [FromBody] UpdateHotelResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateHotelCommandFromResourceAssembler.ToCommandFromResource(hotelId, resource);
        var result = await commandService.Handle(command, cancellationToken);

        return HotelActionResultAssembler.ToActionResultFromUpdateHotelResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            updatedEntity => Ok(HotelResourceFromEntityAssembler.ToResourceFromEntity(updatedEntity))
        );
    }




    [HttpDelete("{hotelId}/staff/{userId}")]
    [SwaggerOperation(
        Summary = "Remove a staff member from a hotel",
        Description = "Remove a staff member from a hotel",
        OperationId = "RemoveHotelStaffMember")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The staff member was removed from the hotel")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The staff member was not found")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The staff member is not assigned to the hotel")]
    public async Task<IActionResult> RemoveHotelStaffMember(
        [FromRoute] string hotelId,
        [FromRoute] string userId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveUserFromHotelCommand(hotelId, userId);
        var result = await _userCommandService.Handle(command, cancellationToken);

        return ActionResultAssembler.ToActionResultFromDeleteResult(
            this,
            result,
            _iamLocalizer,
            _problemDetailsFactory,
            () => NoContent()
        );
    }

}
