using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails;
using Senit.Platform.API.Shared.Interfaces.Rest.Transform;

namespace Senit.Platform.API.GuestStay.Interfaces.Rest.Transform;

/// <summary>
///     Converts consumption use case results to HTTP action results.
/// </summary>
public static class ConsumptionActionResultAssembler
{
    public static IActionResult ToActionResultFromGetConsumptionByIdResult(
        ControllerBase controller,
        Consumption? entity,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Consumption, IActionResult> onFound)
    {
        return ActionResultAssembler.ToActionResultFromGetResult(
            controller,
            entity,
            localizer,
            problemDetailsFactory,
            onFound);
    }

    public static IActionResult ToActionResultFromCreateConsumptionResult(
        ControllerBase controller,
        ApplicationResult<Consumption> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Consumption, IActionResult> onCreated)
    {
        return ActionResultAssembler.ToActionResultFromCommandResult(
            controller,
            result,
            localizer,
            problemDetailsFactory,
            onCreated);
    }

    public static IActionResult ToActionResultFromUpdateConsumptionResult(
        ControllerBase controller,
        ApplicationResult<Consumption> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Consumption, IActionResult> onUpdated)
    {
        return ActionResultAssembler.ToActionResultFromCommandResult(
            controller,
            result,
            localizer,
            problemDetailsFactory,
            onUpdated);
    }

    public static IActionResult ToActionResultFromDeleteConsumptionResult(
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
