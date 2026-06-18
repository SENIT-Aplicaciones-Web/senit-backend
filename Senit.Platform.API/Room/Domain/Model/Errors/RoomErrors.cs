namespace Senit.Platform.API.Room.Domain.Model.Errors;

/// <summary>
///     Room context error codes used by application services.
/// </summary>
public enum RoomErrors
{
    RoomNotFound,
    DuplicateRoomNumber,
    InvalidStatus,
    InvalidRoomType,
    RoomHasActiveStay
}
