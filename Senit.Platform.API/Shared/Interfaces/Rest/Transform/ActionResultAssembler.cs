using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails;

namespace Senit.Platform.API.Shared.Interfaces.Rest.Transform;

/// <summary>
///     Converts application results to HTTP action results.
/// </summary>
public static class ActionResultAssembler
{
    /// <summary>
    ///     Converts a nullable query result to an HTTP action result.
    /// </summary>
    public static IActionResult ToActionResultFromGetResult<TEntity>(
        ControllerBase controller,
        TEntity? entity,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<TEntity, IActionResult> onFound) where TEntity : class
    {
        if (entity is not null)
            return onFound(entity);

        return problemDetailsFactory.CreateErrorProblemDetails(
            controller,
            StatusCodes.Status404NotFound,
            "ResourceNotFound",
            localizer);
    }

    /// <summary>
    ///     Converts a command result to an HTTP action result.
    /// </summary>
    public static IActionResult ToActionResultFromCommandResult<TEntity>(
        ControllerBase controller,
        ApplicationResult<TEntity> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<TEntity, IActionResult> onSuccess) where TEntity : class
    {
        if (result.IsSuccess && result.Value is not null)
            return onSuccess(result.Value);

        return problemDetailsFactory.CreateErrorProblemDetails(
            controller,
            result.StatusCode,
            result.ErrorCode ?? "OperationFailed",
            localizer,
            result.ErrorArguments);
    }

    /// <summary>
    ///     Converts a delete command result to an HTTP action result.
    /// </summary>
    public static IActionResult ToActionResultFromDeleteResult(
        ControllerBase controller,
        ApplicationResult<bool> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<IActionResult> onSuccess)
    {
        if (result.IsSuccess)
            return onSuccess();

        return problemDetailsFactory.CreateErrorProblemDetails(
            controller,
            result.StatusCode,
            result.ErrorCode ?? "OperationFailed",
            localizer,
            result.ErrorArguments);
    }
}
