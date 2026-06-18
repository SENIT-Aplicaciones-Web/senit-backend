using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.GuestStay.Application.CommandServices;

/// <summary>
///     Command service contract for gueststay use cases.
/// </summary>
public interface IGuestStayCommandService
{
    Task<ApplicationResult<GuestStayRecord>> Handle(CreateGuestStayCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<GuestStayRecord>> Handle(UpdateGuestStayCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteGuestStayCommand command, CancellationToken cancellationToken = default);
}
