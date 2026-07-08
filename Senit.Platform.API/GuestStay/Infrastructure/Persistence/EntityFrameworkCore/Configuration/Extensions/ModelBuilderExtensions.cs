using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;

namespace Senit.Platform.API.GuestStay.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the GuestStay bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the GuestStay bounded context database configuration.
    /// </summary>
    /// <param name="builder">
    ///     The model builder used to configure the persistence model.
    /// </param>
    public static void ApplyGuestStayConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Guest>().ToTable("guests");
        builder.Entity<Guest>().HasKey(entity => entity.Id);
        builder.Entity<Guest>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<Guest>().Property(entity => entity.FullName).IsRequired().HasMaxLength(250);
        builder.Entity<Guest>().Property(entity => entity.Dni).IsRequired().HasMaxLength(8);
        builder.Entity<Guest>().Property(entity => entity.Phone).IsRequired().HasMaxLength(9);
        builder.Entity<Guest>().Property(entity => entity.Email).HasMaxLength(250);

        builder.Entity<GuestStayRecord>().ToTable("guest_stays");
        builder.Entity<GuestStayRecord>().HasKey(entity => entity.Id);
        builder.Entity<GuestStayRecord>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<GuestStayRecord>().Property(entity => entity.HotelId).IsRequired().HasMaxLength(64);
        builder.Entity<GuestStayRecord>().Property(entity => entity.RoomId).IsRequired().HasMaxLength(64);
        builder.Entity<GuestStayRecord>().Property(entity => entity.GuestId).IsRequired().HasMaxLength(64);
        builder.Entity<GuestStayRecord>().Property(entity => entity.GuestName).IsRequired().HasMaxLength(250);
        builder.Entity<GuestStayRecord>().Property(entity => entity.AdditionalGuestsJson).HasColumnType("longtext");
        builder.Entity<GuestStayRecord>().Property(entity => entity.Status).IsRequired().HasMaxLength(50);
        builder.Entity<GuestStayRecord>().Property(entity => entity.BaseAmount).HasPrecision(10, 2);
        builder.Entity<GuestStayRecord>().Property(entity => entity.AdditionalAmount).HasPrecision(10, 2);
        builder.Entity<GuestStayRecord>().Property(entity => entity.PrepaidAmount).HasPrecision(10, 2);
        builder.Entity<GuestStayRecord>().Property(entity => entity.TotalAmount).HasPrecision(10, 2);
        builder.Entity<GuestStayRecord>().Property(entity => entity.PaymentStatus).IsRequired().HasMaxLength(50);

        builder.Entity<GuestStayRecord>()
            .HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(entity => entity.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<GuestStayRecord>()
            .HasOne<RoomEntity>()
            .WithMany()
            .HasForeignKey(entity => entity.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<GuestStayRecord>()
            .HasOne<Guest>()
            .WithMany()
            .HasForeignKey(entity => entity.GuestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Consumption>().ToTable("consumptions");
        builder.Entity<Consumption>().HasKey(entity => entity.Id);
        builder.Entity<Consumption>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<Consumption>().Property(entity => entity.GuestStayId).IsRequired().HasMaxLength(64);
        builder.Entity<Consumption>().Property(entity => entity.Description).IsRequired().HasMaxLength(250);
        builder.Entity<Consumption>().Property(entity => entity.UnitPrice).HasPrecision(10, 2);
        builder.Entity<Consumption>().Property(entity => entity.Amount).HasPrecision(10, 2);

        builder.Entity<Consumption>()
            .HasOne<GuestStayRecord>()
            .WithMany()
            .HasForeignKey(entity => entity.GuestStayId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
