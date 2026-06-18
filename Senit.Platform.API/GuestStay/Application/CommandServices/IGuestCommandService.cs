using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Commands;
using Senit.Platform.API.Shared.Application.Model;

namespace Senit.Platform.API.GuestStay.Application.CommandServices;

/// <summary>
///     Command service contract for guest use cases.
/// </summary>
public interface IGuestCommandService
{
    Task<ApplicationResult<Guest>> Handle(CreateGuestCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<Guest>> Handle(UpdateGuestCommand command, CancellationToken cancellationToken = default);

    Task<ApplicationResult<bool>> Handle(DeleteGuestCommand command, CancellationToken cancellationToken = default);
}
