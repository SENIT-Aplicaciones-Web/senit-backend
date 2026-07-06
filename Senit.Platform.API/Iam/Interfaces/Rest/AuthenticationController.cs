using System.Net.Mime;
using Senit.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Senit.Platform.API.Iam.Application.CommandServices;
using Senit.Platform.API.Iam.Interfaces.Rest.Resources;
using Senit.Platform.API.Iam.Interfaces.Rest.Transform;
using Senit.Platform.API.Iam.Resources;
using Senit.Platform.API.Shared.Interfaces.Rest.Transform;
using ProblemDetailsFactory = Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace Senit.Platform.API.Iam.Interfaces.Rest;

/// <summary>
///     REST controller for JWT authentication.
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
        Description = "Sign in and obtain a JWT bearer token",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The credentials are invalid")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(
        [FromBody] SignInResource resource,
        CancellationToken cancellationToken)
    {
        var command = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await authenticationCommandService.Handle(command, cancellationToken);

        return UserActionResultAssembler.ToActionResultFromAuthenticatedUserResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            authenticatedUser => Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(
                authenticatedUser.user,
                authenticatedUser.token))
        );
    }

    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Sign up",
        Description = "Register a new hotel administrator and obtain a JWT bearer token",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status201Created, "The hotel and administrator were created", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The email is already registered")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(
        [FromBody] SignUpResource resource,
        CancellationToken cancellationToken)
    {
        var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await authenticationCommandService.Handle(command, cancellationToken);

        return UserActionResultAssembler.ToActionResultFromAuthenticatedUserResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            createdUser => StatusCode(StatusCodes.Status201Created,
                AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(createdUser.user, createdUser.token))
        );
    }

    [HttpPost("reset-password")]
    [SwaggerOperation(
        Summary = "Reset password",
        Description = "Reset a user password by email from the public authentication flow",
        OperationId = "ResetPassword")]
    [SwaggerResponse(StatusCodes.Status200OK, "The password was reset")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The user was not found")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetPasswordResource resource,
        CancellationToken cancellationToken)
    {
        var command = ResetPasswordCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await authenticationCommandService.Handle(command, cancellationToken);

        return ActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            _contextLocalizer,
            _problemDetailsFactory,
            _ => Ok(new { message = _contextLocalizer["PasswordResetSuccessfully"].Value })
        );
    }
}
