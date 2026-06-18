using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.FrontDesk.Domain.Model.Queries;

namespace Senit.Platform.API.FrontDesk.Application.QueryServices;

/// <summary>
///     Query service contract for notification use cases.
/// </summary>
public interface INotificationQueryService
{
    Task<IEnumerable<Notification>> Handle(GetAllNotificationsQuery query, CancellationToken cancellationToken = default);

    Task<Notification?> Handle(GetNotificationByIdQuery query, CancellationToken cancellationToken = default);
}
