using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails;
using Senit.Platform.API.Shared.Interfaces.Rest.Transform;

using Senit.Platform.API.Reservation.Domain.Model.Aggregates;
namespace Senit.Platform.API.Reservation.Interfaces.Rest.Transform;

/// <summary>
///     Converts reservation use case results to HTTP action results.
/// </summary>
public static class ReservationActionResultAssembler
{
    public static IActionResult ToActionResultFromGetReservationByIdResult(
        ControllerBase controller,
        HotelReservation? entity,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<HotelReservation, IActionResult> onFound)
    {
        return ActionResultAssembler.ToActionResultFromGetResult(
            controller,
            entity,
            localizer,
            problemDetailsFactory,
            onFound);
    }

    public static IActionResult ToActionResultFromCreateReservationResult(
        ControllerBase controller,
        ApplicationResult<HotelReservation> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<HotelReservation, IActionResult> onCreated)
    {
        return ActionResultAssembler.ToActionResultFromCommandResult(
            controller,
            result,
            localizer,
            problemDetailsFactory,
            onCreated);
    }

    public static IActionResult ToActionResultFromUpdateReservationResult(
        ControllerBase controller,
        ApplicationResult<HotelReservation> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<HotelReservation, IActionResult> onUpdated)
    {
        return ActionResultAssembler.ToActionResultFromCommandResult(
            controller,
            result,
            localizer,
            problemDetailsFactory,
            onUpdated);
    }

    public static IActionResult ToActionResultFromDeleteReservationResult(
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
