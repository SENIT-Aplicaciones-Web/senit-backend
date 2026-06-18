using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Queries;

namespace Senit.Platform.API.GuestStay.Application.QueryServices;

/// <summary>
///     Query service contract for guest use cases.
/// </summary>
public interface IGuestQueryService
{
    Task<IEnumerable<Guest>> Handle(GetAllGuestsQuery query, CancellationToken cancellationToken = default);

    Task<Guest?> Handle(GetGuestByIdQuery query, CancellationToken cancellationToken = default);
}
