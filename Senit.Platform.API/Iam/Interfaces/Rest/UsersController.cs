using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.Iam.Application.CommandServices;
using Senit.Platform.API.Iam.Application.QueryServices;
using Senit.Platform.API.Iam.Domain.Model.Queries;
using Senit.Platform.API.Iam.Interfaces.Rest.Resources;
using Senit.Platform.API.Iam.Interfaces.Rest.Transform;
using Senit.Platform.API.Iam.Resources;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.Iam.Interfaces.Rest;

/// <summary>
///     REST controller for users.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Users endpoints")]
public class UsersController(
    IUserQueryService queryService,
    IUserCommandService commandService,
    IStringLocalizer<IamMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<IamMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    // User deletion is not exposed here. Staff removal is handled by hotels/{hotelId}/staff/{userId}.

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "Get all users",
        OperationId = "GetAllUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "The users were found", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var entities = await queryService.Handle(query, cancellationToken);
        var resources = entities.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("{userId}")]
    [SwaggerOperation(
        Summary = "Get a user by id",
        Description = "Get a user by id",
        OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was found", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The user was not found")]
    public async Task<IActionResult> GetUserById([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);
        var entity = await queryService.Handle(query, cancellationToken);

        return UserActionResultAssembler.ToActionResultFromGetUserByIdResult(
            this,
            entity,
            _contextLocalizer,
            _problemDetailsFactory,
            foundEntity => Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(foundEntity))
        );
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a user",
        Description = "Create a user",
        OperationId = "CreateUser")]
    [SwaggerResponse(StatusCodes.Status201Created, "The user was created", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The user was not created")]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return UserActionResultAssembler.ToActionResultFromCreateUserResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdEntity => CreatedAtAction(
                nameof(GetUserById),
                new { userId = createdEntity.Id },
                UserResourceFromEntityAssembler.ToResourceFromEntity(createdEntity))
        );
    }

    [HttpPut("{userId}")]
    [SwaggerOperation(
        Summary = "Update a user",
        Description = "Update a user",
        OperationId = "UpdateUser")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was updated", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The user was not found")]
    public async Task<IActionResult> UpdateUser(
        [FromRoute] string userId,
        [FromBody] UpdateUserResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(userId, resource);
        var result = await commandService.Handle(command, cancellationToken);

        return UserActionResultAssembler.ToActionResultFromUpdateUserResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            updatedEntity => Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(updatedEntity))
        );
    }
}
