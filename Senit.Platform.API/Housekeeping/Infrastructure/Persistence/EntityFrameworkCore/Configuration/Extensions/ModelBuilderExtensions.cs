using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.Housekeeping.Domain.Model.Aggregates;
using RoomEntity = Senit.Platform.API.Room.Domain.Model.Aggregates.Room;

namespace Senit.Platform.API.Housekeeping.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Housekeeping bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Housekeeping bounded context database configuration.
    /// </summary>
    /// <param name="builder">
    ///     The model builder used to configure the persistence model.
    /// </param>
    public static void ApplyHousekeepingConfiguration(this ModelBuilder builder)
    {
        builder.Entity<CleaningTask>().ToTable("cleaning_tasks");
        builder.Entity<CleaningTask>().HasKey(entity => entity.Id);
        builder.Entity<CleaningTask>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<CleaningTask>().Property(entity => entity.HotelId).IsRequired().HasMaxLength(64);
        builder.Entity<CleaningTask>().Property(entity => entity.RoomId).IsRequired().HasMaxLength(64);
        builder.Entity<CleaningTask>().Property(entity => entity.Description).IsRequired().HasMaxLength(250);
        builder.Entity<CleaningTask>().Property(entity => entity.Status).IsRequired().HasMaxLength(50);

        builder.Entity<CleaningTask>()
            .HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(entity => entity.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CleaningTask>()
            .HasOne<RoomEntity>()
            .WithMany()
            .HasForeignKey(entity => entity.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
