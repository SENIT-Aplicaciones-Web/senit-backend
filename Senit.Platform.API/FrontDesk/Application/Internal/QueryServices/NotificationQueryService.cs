using Senit.Platform.API.FrontDesk.Application.QueryServices;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.FrontDesk.Domain.Model.Queries;
using Senit.Platform.API.FrontDesk.Domain.Repositories;

namespace Senit.Platform.API.FrontDesk.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for notification use cases.
/// </summary>
public class NotificationQueryService(INotificationRepository repository) : INotificationQueryService
{
    public async Task<IEnumerable<Notification>> Handle(GetAllNotificationsQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.ListAsync(cancellationToken);
    }

    public async Task<Notification?> Handle(GetNotificationByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindByIdAsync(query.Id, cancellationToken);
    }
}
