using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Queries;

namespace Senit.Platform.API.GuestStay.Application.QueryServices;

/// <summary>
///     Query service contract for gueststay use cases.
/// </summary>
public interface IGuestStayQueryService
{
    Task<IEnumerable<GuestStayRecord>> Handle(GetAllGuestStaysQuery query, CancellationToken cancellationToken = default);

    Task<GuestStayRecord?> Handle(GetGuestStayByIdQuery query, CancellationToken cancellationToken = default);
}
