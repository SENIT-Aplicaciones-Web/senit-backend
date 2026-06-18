using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Domain.Model.Aggregates;

namespace Senit.Platform.API.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the IAM bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the IAM bounded context database configuration.
    /// </summary>
    /// <param name="builder">
    ///     The model builder used to configure the persistence model.
    /// </param>
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("users");
        builder.Entity<User>().HasKey(entity => entity.Id);
        builder.Entity<User>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<User>().Ignore(entity => entity.HotelId);
        builder.Entity<User>().Property(entity => entity.FullName).IsRequired().HasMaxLength(250);
        builder.Entity<User>().Property(entity => entity.Username).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(entity => entity.Email).IsRequired().HasMaxLength(250);
        builder.Entity<User>().Property(entity => entity.Password).IsRequired().HasMaxLength(250);
        builder.Entity<User>().Property(entity => entity.Role).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(entity => entity.Status).IsRequired().HasMaxLength(50);

        builder.Entity<HotelStaffMember>().ToTable("hotel_staff_members");
        builder.Entity<HotelStaffMember>().HasKey(entity => entity.Id);
        builder.Entity<HotelStaffMember>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<HotelStaffMember>().Property(entity => entity.HotelId).IsRequired().HasMaxLength(64);
        builder.Entity<HotelStaffMember>().Property(entity => entity.UserId).IsRequired().HasMaxLength(64);
        builder.Entity<HotelStaffMember>().Property(entity => entity.Role).IsRequired().HasMaxLength(50);
        builder.Entity<HotelStaffMember>().Property(entity => entity.Status).IsRequired().HasMaxLength(50);

        builder.Entity<HotelStaffMember>()
            .HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(entity => entity.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<HotelStaffMember>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(entity => entity.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
