namespace Senit.Platform.API.FrontDesk.Interfaces.Acl;

/// <summary>
///     Anti corruption facade exposed by the FrontDesk bounded context.
/// </summary>
public interface IFrontDeskContextFacade
{
    /// <summary>
    ///     Creates a hotel and returns its identifier when successful.
    /// </summary>
    Task<string> CreateHotel(
        string name,
        string ruc,
        string address,
        string phone,
        string email,
        string plan,
        string status,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks whether a hotel exists without exposing FrontDesk domain objects.
    /// </summary>
    Task<bool> HotelExists(string hotelId, CancellationToken cancellationToken = default);
    /// <summary>
    ///     Updates a hotel plan and status without exposing FrontDesk domain objects.
    /// </summary>
    Task<bool> UpdateHotelPlanAndStatus(
        string hotelId,
        string plan,
        string status,
        CancellationToken cancellationToken = default);

}
