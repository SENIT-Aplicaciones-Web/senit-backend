using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Resources.Errors;

namespace Senit.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
///     Authorization filter used by protected controllers and actions.
///     It checks the authenticated user loaded by RequestAuthorizationMiddleware.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    ///     Validates that the current request has an authenticated user.
    /// </summary>
    /// <param name="context">The authorization filter context.</param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata
            .OfType<AllowAnonymousAttribute>()
            .Any();

        if (allowAnonymous) return;

        var user = (User?)context.HttpContext.Items["User"];

        if (user is null)
        {
            var errorLocalizer = context.HttpContext.RequestServices
                .GetService<IStringLocalizer<ErrorMessages>>();

            context.Result = new UnauthorizedObjectResult(new
            {
                code = "Unauthorized",
                message = errorLocalizer?["UnauthorizedJwt"].Value
                    ?? "Unauthorized missing expired or invalid JWT bearer token"
            });
        }
    }
}
