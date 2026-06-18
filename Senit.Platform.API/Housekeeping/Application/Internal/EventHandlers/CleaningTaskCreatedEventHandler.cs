using Senit.Platform.API.Housekeeping.Domain.Model.Events;
using Senit.Platform.API.Shared.Application.Internal.EventHandlers;

namespace Senit.Platform.API.Housekeeping.Application.Internal.EventHandlers;

/// <summary>
///     Handles the cleaningtask created event.
/// </summary>
public class CleaningTaskCreatedEventHandler : IEventHandler<CleaningTaskCreatedEvent>
{
    public Task Handle(CleaningTaskCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(CleaningTaskCreatedEvent domainEvent)
    {
        Console.WriteLine("Created CleaningTask: {0}", domainEvent.Summary);
        return Task.CompletedTask;
    }
}
