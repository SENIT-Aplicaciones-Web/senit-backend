using Senit.Platform.API.Shared.Domain.Model.Events;

namespace Senit.Platform.API.Housekeeping.Domain.Model.Events;

/// <summary>
///     Event raised when a cleaningtask is created.
/// </summary>
public class CleaningTaskCreatedEvent(string id, string summary) : IEvent
{
    public string Id { get; } = id;

    public string Summary { get; } = summary;
}
