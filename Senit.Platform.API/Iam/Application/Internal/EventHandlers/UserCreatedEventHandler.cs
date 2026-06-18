using Senit.Platform.API.Iam.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.Iam.Application.Internal.EventHandlers;

/// <summary>
///     Handles the user created event.
/// </summary>
public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
{
    public Task Handle(UserCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(UserCreatedEvent domainEvent)
    {
        Console.WriteLine("Created User: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
