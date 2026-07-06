using Senit.Platform.API.Iam.Application.Internal.OutboundServices;
using Senit.Platform.API.Iam.Application.QueryServices;
using Senit.Platform.API.Iam.Domain.Model.Queries;
using Senit.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace Senit.Platform.API.Iam.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
///     Middleware that reads a JWT bearer token from the Authorization header,
///     validates it and stores the authenticated user in HttpContext.Items["User"].
/// </summary>
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    /// <summary>
    ///     Authorizes the current request when a bearer token is available.
    /// </summary>
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        var allowAnonymous = context.GetEndpoint()?.Metadata
            .Any(metadata => metadata.GetType() == typeof(AllowAnonymousAttribute)) ?? false;

        if (allowAnonymous)
        {
            await next(context);
            return;
        }

        var token = context.Request.Headers.Authorization
            .FirstOrDefault()?
            .Split(' ')
            .Last();

        if (!string.IsNullOrWhiteSpace(token))
        {
            var userId = await tokenService.ValidateToken(token);

            if (!string.IsNullOrWhiteSpace(userId))
            {
                var user = await userQueryService.Handle(new GetUserByIdQuery(userId), context.RequestAborted);
                if (user is not null)
                    context.Items["User"] = user;
            }
        }

        await next(context);
    }
}
