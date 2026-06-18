using Senit.Platform.API.Shared.Domain.Model.Events;

namespace Senit.Platform.API.GuestStay.Domain.Model.Events;

/// <summary>
///     Event raised when a consumption is created.
/// </summary>
public class ConsumptionCreatedEvent(string id, string summary) : IEvent
{
    public string Id { get; } = id;

    public string Summary { get; } = summary;
}
