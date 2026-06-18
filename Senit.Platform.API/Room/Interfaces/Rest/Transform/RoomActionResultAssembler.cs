using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails;
using Senit.Platform.API.Shared.Interfaces.Rest.Transform;

namespace Senit.Platform.API.Room.Interfaces.Rest.Transform;

/// <summary>
///     Converts room use case results to HTTP action results.
/// </summary>
public static class RoomActionResultAssembler
{
    public static IActionResult ToActionResultFromGetRoomByIdResult(
        ControllerBase controller,
        RoomEntity? entity,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<RoomEntity, IActionResult> onFound)
    {
        return ActionResultAssembler.ToActionResultFromGetResult(
            controller,
            entity,
            localizer,
            problemDetailsFactory,
            onFound);
    }

    public static IActionResult ToActionResultFromCreateRoomResult(
        ControllerBase controller,
        ApplicationResult<RoomEntity> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<RoomEntity, IActionResult> onCreated)
    {
        return ActionResultAssembler.ToActionResultFromCommandResult(
            controller,
            result,
            localizer,
            problemDetailsFactory,
            onCreated);
    }

    public static IActionResult ToActionResultFromUpdateRoomResult(
        ControllerBase controller,
        ApplicationResult<RoomEntity> result,
        IStringLocalizer localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<RoomEntity, IActionResult> onUpdated)
    {
        return ActionResultAssembler.ToActionResultFromCommandResult(
            controller,
            result,
            localizer,
            problemDetailsFactory,
            onUpdated);
    }

    public static IActionResult ToActionResultFromDeleteRoomResult(
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
