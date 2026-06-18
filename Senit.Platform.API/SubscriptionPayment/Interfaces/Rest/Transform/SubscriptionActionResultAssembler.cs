using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails;
using Senit.Platform.API.Shared.Interfaces.Rest.Transform;

namespace Senit.Platform.API.SubscriptionPayment.Interfaces.Rest.Transform;

/// <summary>
///     Converts subscription use case results to HTTP action results.
/// </summary>
public static class SubscriptionActionResultAssembler
{
    public static IActionResult ToActionResultFromGetSubscriptionByIdResult(
        ControllerBase controller,
        Subscription? entity,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Subscription, IActionResult> onFound)
    {
        return ActionResultAssembler.ToActionResultFromGetResult(
            controller,
            entity,
            localizer,
            problemDetailsFactory,
            onFound);
    }

    public static IActionResult ToActionResultFromCreateSubscriptionResult(
        ControllerBase controller,
        ApplicationResult<Subscription> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Subscription, IActionResult> onCreated)
    {
        return ActionResultAssembler.ToActionResultFromCommandResult(
            controller,
            result,
            localizer,
            problemDetailsFactory,
            onCreated);
    }

    public static IActionResult ToActionResultFromUpdateSubscriptionResult(
        ControllerBase controller,
        ApplicationResult<Subscription> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Subscription, IActionResult> onUpdated)
    {
        return ActionResultAssembler.ToActionResultFromCommandResult(
            controller,
            result,
            localizer,
            problemDetailsFactory,
            onUpdated);
    }

    public static IActionResult ToActionResultFromDeleteSubscriptionResult(
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
