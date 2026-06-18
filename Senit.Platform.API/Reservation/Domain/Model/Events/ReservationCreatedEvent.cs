using Senit.Platform.API.Shared.Domain.Model.Events;

namespace Senit.Platform.API.Reservation.Domain.Model.Events;

/// <summary>
///     Event raised when a reservation is created.
/// </summary>
public class ReservationCreatedEvent(string id, string summary) : IEvent
{
    public string Id { get; } = id;

    public string Summary { get; } = summary;
}
