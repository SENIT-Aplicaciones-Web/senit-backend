using Senit.Platform.API.Shared.Domain.Model.Events;

namespace Senit.Platform.API.Room.Domain.Model.Events;

/// <summary>
///     Event raised when a room is created.
/// </summary>
public class RoomCreatedEvent(string id, string summary) : IEvent
{
    public string Id { get; } = id;

    public string Summary { get; } = summary;
}
