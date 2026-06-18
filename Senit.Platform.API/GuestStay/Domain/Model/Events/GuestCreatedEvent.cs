using Senit.Platform.API.Shared.Domain.Model.Events;

namespace Senit.Platform.API.GuestStay.Domain.Model.Events;

/// <summary>
///     Event raised when a guest is created.
/// </summary>
public class GuestCreatedEvent(string id, string summary) : IEvent
{
    public string Id { get; } = id;

    public string Summary { get; } = summary;
}
