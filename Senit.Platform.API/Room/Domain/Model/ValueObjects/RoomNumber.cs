namespace Senit.Platform.API.Room.Domain.Model.ValueObjects;

/// <summary>
///     Represents the room number assigned inside a hotel.
/// </summary>
/// <param name="Value">The room number.</param>
public readonly record struct RoomNumber(string Value)
{
    public static bool IsValid(string value) => !string.IsNullOrWhiteSpace(value);
}
