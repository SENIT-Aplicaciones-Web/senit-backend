using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails;
using Senit.Platform.API.Shared.Interfaces.Rest.Transform;

namespace Senit.Platform.API.Housekeeping.Interfaces.Rest.Transform;

/// <summary>
///     Converts cleaningtask use case results to HTTP action results.
/// </summary>
public static class CleaningTaskActionResultAssembler
{
    public static IActionResult ToActionResultFromGetCleaningTaskByIdResult(
        ControllerBase controller,
        CleaningTask? entity,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<CleaningTask, IActionResult> onFound)
    {
        return ActionResultAssembler.ToActionResultFromGetResult(
            controller,
            entity,
            localizer,
            problemDetailsFactory,
            onFound);
    }

    public static IActionResult ToActionResultFromCreateCleaningTaskResult(
        ControllerBase controller,
        ApplicationResult<CleaningTask> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<CleaningTask, IActionResult> onCreated)
    {
        return ActionResultAssembler.ToActionResultFromCommandResult(
            controller,
            result,
            localizer,
            problemDetailsFactory,
            onCreated);
    }

    public static IActionResult ToActionResultFromUpdateCleaningTaskResult(
        ControllerBase controller,
        ApplicationResult<CleaningTask> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<CleaningTask, IActionResult> onUpdated)
    {
        return ActionResultAssembler.ToActionResultFromCommandResult(
            controller,
            result,
            localizer,
            problemDetailsFactory,
            onUpdated);
    }

    public static IActionResult ToActionResultFromDeleteCleaningTaskResult(
        ControllerBase controller,
        ApplicationResult<bool> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<IActionResult> onDeleted)
    {
        return ActionResultAssembler.ToActionResultFromDeleteResult(
            controller,
            result,
            localizer,
            problemDetailsFactory,
            onDeleted);
    }
}
