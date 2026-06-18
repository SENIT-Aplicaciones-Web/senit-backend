using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;

namespace Senit.Platform.API.Room.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Room bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Room bounded context database configuration.
    /// </summary>
    /// <param name="builder">
    ///     The model builder used to configure the persistence model.
    /// </param>
    public static void ApplyRoomConfiguration(this ModelBuilder builder)
    {
        builder.Entity<RoomEntity>().ToTable("rooms");
        builder.Entity<RoomEntity>().HasKey(entity => entity.Id);
        builder.Entity<RoomEntity>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<RoomEntity>().Property(entity => entity.HotelId).IsRequired().HasMaxLength(64);
        builder.Entity<RoomEntity>().Property(entity => entity.Number).IsRequired().HasMaxLength(20);
        builder.Entity<RoomEntity>().Property(entity => entity.Type).IsRequired().HasMaxLength(100);
        builder.Entity<RoomEntity>().Property(entity => entity.PricePerHour).HasPrecision(10, 2);
        builder.Entity<RoomEntity>().Property(entity => entity.Status).IsRequired().HasMaxLength(50);

        builder.Entity<RoomEntity>()
            .HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(entity => entity.HotelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
