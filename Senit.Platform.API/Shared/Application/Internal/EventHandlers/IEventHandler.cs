using Cortex.Mediator.Notifications;
using Senit.Platform.API.Shared.Domain.Model.Events;

namespace Senit.Platform.API.Shared.Application.Internal.EventHandlers;

/// <summary>
///     Contract for domain event handlers.
/// </summary>
/// <typeparam name="TEvent">
///     The event type.
/// </typeparam>
public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}
