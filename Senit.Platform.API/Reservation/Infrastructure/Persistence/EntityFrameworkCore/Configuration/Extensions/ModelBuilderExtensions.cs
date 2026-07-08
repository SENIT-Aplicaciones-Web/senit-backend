using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.Reservation.Domain.Model.Aggregates;
using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;

namespace Senit.Platform.API.Reservation.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Reservation bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Reservation bounded context database configuration.
    /// </summary>
    /// <param name="builder">
    ///     The model builder used to configure the persistence model.
    /// </param>
    public static void ApplyReservationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<HotelReservation>().ToTable("reservations");
        builder.Entity<HotelReservation>().HasKey(entity => entity.Id);
        builder.Entity<HotelReservation>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<HotelReservation>().Property(entity => entity.HotelId).IsRequired().HasMaxLength(64);
        builder.Entity<HotelReservation>().Property(entity => entity.RoomId).IsRequired().HasMaxLength(64);
        builder.Entity<HotelReservation>().Property(entity => entity.GuestName).IsRequired().HasMaxLength(250);
        builder.Entity<HotelReservation>().Property(entity => entity.Dni).IsRequired().HasMaxLength(8);
        builder.Entity<HotelReservation>().Property(entity => entity.Phone).IsRequired().HasMaxLength(9);
        builder.Entity<HotelReservation>().Property(entity => entity.Email).HasMaxLength(250);
        builder.Entity<HotelReservation>().Property(entity => entity.AdditionalGuestsJson).HasColumnType("longtext");
        builder.Entity<HotelReservation>().Property(entity => entity.Status).IsRequired().HasMaxLength(50);
        builder.Entity<HotelReservation>().Property(entity => entity.Hours).HasPrecision(10, 2);
        builder.Entity<HotelReservation>().Property(entity => entity.ReservationAmount).HasPrecision(10, 2);
        builder.Entity<HotelReservation>().Property(entity => entity.PrepaidAmount).HasPrecision(10, 2);
        builder.Entity<HotelReservation>().Property(entity => entity.PaymentMethod).IsRequired().HasMaxLength(50);
        builder.Entity<HotelReservation>().Property(entity => entity.PaymentStatus).IsRequired().HasMaxLength(50);

        builder.Entity<HotelReservation>()
            .HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(entity => entity.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<HotelReservation>()
            .HasOne<RoomEntity>()
            .WithMany()
            .HasForeignKey(entity => entity.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
