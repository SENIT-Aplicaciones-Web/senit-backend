using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.FrontDesk.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.FrontDesk.Application.CommandServices;

/// <summary>
///     Command service contract for notification use cases.
/// </summary>
public interface INotificationCommandService
{
    Task<ApplicationResult<Notification>> Handle(CreateNotificationCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<Notification>> Handle(UpdateNotificationCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteNotificationCommand command, CancellationToken cancellationToken = default);
}
