using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;

namespace Senit.Platform.API.FrontDesk.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the FrontDesk bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the FrontDesk bounded context database configuration.
    /// </summary>
    /// <param name="builder">
    ///     The model builder used to configure the persistence model.
    /// </param>
    public static void ApplyFrontDeskConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Hotel>().ToTable("hotels");
        builder.Entity<Hotel>().HasKey(entity => entity.Id);
        builder.Entity<Hotel>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<Hotel>().Property(entity => entity.Name).IsRequired().HasMaxLength(250);
        builder.Entity<Hotel>().Property(entity => entity.Ruc).HasMaxLength(20);
        builder.Entity<Hotel>().Property(entity => entity.Address).HasMaxLength(250);
        builder.Entity<Hotel>().Property(entity => entity.Phone).HasMaxLength(20);
        builder.Entity<Hotel>().Property(entity => entity.Email).HasMaxLength(250);
        builder.Entity<Hotel>().Property(entity => entity.Plan).IsRequired().HasMaxLength(50);
        builder.Entity<Hotel>().Property(entity => entity.Status).IsRequired().HasMaxLength(50);

        builder.Entity<Notification>().ToTable("notifications");
        builder.Entity<Notification>().HasKey(entity => entity.Id);
        builder.Entity<Notification>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<Notification>().Property(entity => entity.HotelId).IsRequired().HasMaxLength(64);
        builder.Entity<Notification>().Property(entity => entity.Title).IsRequired().HasMaxLength(250);
        builder.Entity<Notification>().Property(entity => entity.Message).IsRequired().HasMaxLength(500);
        builder.Entity<Notification>().Property(entity => entity.Type).IsRequired().HasMaxLength(50);
        builder.Entity<Notification>().Property(entity => entity.CreatedBy).HasMaxLength(64);

        builder.Entity<Notification>()
            .HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(entity => entity.HotelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
