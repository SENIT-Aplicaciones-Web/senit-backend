using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
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
///     REST controller for notifications.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available visible notification endpoints")]
public class NotificationsController(
    INotificationQueryService queryService,
    INotificationCommandService commandService,
    IStringLocalizer<FrontDeskMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<FrontDeskMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // Notifications represent operational events exposed to the frontend as visible hotel alerts.

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all visible notifications",
        Description = "Get visible notifications. When hotelId is provided, only notifications belonging to the requested hotel are returned.",
        OperationId = "GetAllNotifications")]
    [SwaggerResponse(StatusCodes.Status200OK, "The notifications were found", typeof(IEnumerable<NotificationResource>))]
    public async Task<IActionResult> GetAllNotifications(
        [SwaggerParameter("Hotel identifier used to return only visible notifications owned by the active hotel.", Required = false)]
        [FromQuery] string? hotelId,
        CancellationToken cancellationToken)
    {
        var query = new GetAllNotificationsQuery(hotelId);
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a visible notification",
        Description = "Create a visible notification",
        OperationId = "CreateNotification")]
    [SwaggerResponse(StatusCodes.Status201Created, "The notification was created", typeof(NotificationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The notification was not created")]
    public async Task<IActionResult> CreateNotification(
        [FromBody] CreateNotificationResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateNotificationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return NotificationActionResultAssembler.ToActionResultFromCreateNotificationResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdEntity => StatusCode(
                StatusCodes.Status201Created,
                NotificationResourceFromEntityAssembler.ToResourceFromEntity(createdEntity))
        );
    }



}
