using Senit.Platform.API.FrontDesk.Application.CommandServices;
using Senit.Platform.API.FrontDesk.Application.QueryServices;
using Senit.Platform.API.FrontDesk.Domain.Model.Commands;
using Senit.Platform.API.FrontDesk.Domain.Model.Queries;
using Senit.Platform.API.FrontDesk.Interfaces.Acl;

namespace Senit.Platform.API.FrontDesk.Application.Acl;

/// <summary>
///     Anti corruption facade for the FrontDesk bounded context.
/// </summary>
public class FrontDeskContextFacade(
    IHotelCommandService hotelCommandService,
    IHotelQueryService hotelQueryService) : IFrontDeskContextFacade
{
    /// <summary>
    ///     Creates a hotel through the FrontDesk bounded context.
    /// </summary>
    public async Task<string> CreateHotel(
        string name,
        string ruc,
        string address,
        string phone,
        string email,
        string plan,
        string status,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateHotelCommand(name, ruc, address, phone, email, plan, status);
        var result = await hotelCommandService.Handle(command, cancellationToken);
        return result.IsSuccess ? result.Value?.Id ?? string.Empty : string.Empty;
    }

    /// <summary>
    ///     Checks whether a hotel exists in the FrontDesk bounded context.
    /// </summary>
    public async Task<bool> HotelExists(string hotelId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(hotelId)) return false;
        var hotel = await hotelQueryService.Handle(new GetHotelByIdQuery(hotelId), cancellationToken);
        return hotel is not null;
    }
}
