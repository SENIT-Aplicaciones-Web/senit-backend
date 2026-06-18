using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.FrontDesk.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Senit.Platform.API.GuestStay.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Senit.Platform.API.Housekeeping.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Senit.Platform.API.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Senit.Platform.API.Payment.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Senit.Platform.API.Reservation.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Senit.Platform.API.Room.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Senit.Platform.API.SubscriptionPayment.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context.
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyFrontDeskConfiguration();
        builder.ApplyIamConfiguration();
        builder.ApplyRoomConfiguration();
        builder.ApplyReservationConfiguration();
        builder.ApplyGuestStayConfiguration();
        builder.ApplyPaymentConfiguration();
        builder.ApplyHousekeepingConfiguration();
        builder.ApplySubscriptionPaymentConfiguration();

        builder.UseSnakeCaseNamingConvention();
    }
}
