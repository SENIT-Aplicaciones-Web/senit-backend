using Senit.Platform.API.Shared.Domain.Model.Events;

namespace Senit.Platform.API.Iam.Domain.Model.Events;

/// <summary>
///     Event raised when a user is created.
/// </summary>
public class UserCreatedEvent(string id, string summary) : IEvent
{
    public string Id { get; } = id;

    public string Summary { get; } = summary;
}
