using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.Iam.Application.CommandServices;
using Senit.Platform.API.Iam.Interfaces.Rest.Resources;
using Senit.Platform.API.Iam.Interfaces.Rest.Transform;
using Senit.Platform.API.Iam.Resources;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.Iam.Interfaces.Rest;

/// <summary>
///     REST controller for basic authentication.
/// </summary>
[ApiController]
[Route("api/v1/authentication")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(
    IAuthenticationCommandService authenticationCommandService,
    IStringLocalizer<IamMessages> contextLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<IamMessages> _contextLocalizer = contextLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpPost("sign-in")]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Sign in without JWT for the current project advance",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The credentials are invalid")]
    public async Task<IActionResult> SignIn(
        [FromBody] SignInResource resource,
        CancellationToken cancellationToken)
    {
        var command = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await authenticationCommandService.Handle(command, cancellationToken);

        return UserActionResultAssembler.ToActionResultFromCreateUserResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            authenticatedUser => Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser))
        );
    }

    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Sign up",
        Description = "Register a new hotel and administrator without JWT for the current project advance",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status201Created, "The hotel and administrator were created", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The email is already registered")]
    public async Task<IActionResult> SignUp(
        [FromBody] SignUpResource resource,
        CancellationToken cancellationToken)
    {
        var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await authenticationCommandService.Handle(command, cancellationToken);

        return UserActionResultAssembler.ToActionResultFromCreateUserResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdUser => StatusCode(StatusCodes.Status201Created,
                UserResourceFromEntityAssembler.ToResourceFromEntity(createdUser))
        );
    }

}
