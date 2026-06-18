using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.SubscriptionPayment.Domain.Model.Aggregates;

namespace Senit.Platform.API.SubscriptionPayment.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the SubscriptionPayment bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the SubscriptionPayment bounded context database configuration.
    /// </summary>
    /// <param name="builder">
    ///     The model builder used to configure the persistence model.
    /// </param>
    public static void ApplySubscriptionPaymentConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Subscription>().ToTable("subscriptions");
        builder.Entity<Subscription>().HasKey(entity => entity.Id);
        builder.Entity<Subscription>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<Subscription>().Property(entity => entity.HotelId).IsRequired().HasMaxLength(64);
        builder.Entity<Subscription>().Property(entity => entity.Plan).IsRequired().HasMaxLength(50);
        builder.Entity<Subscription>().Property(entity => entity.Status).IsRequired().HasMaxLength(50);
        builder.Entity<Subscription>().Property(entity => entity.MonthlyAmount).HasPrecision(10, 2);

        builder.Entity<Subscription>()
            .HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(entity => entity.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SubscriptionPaymentRecord>().ToTable("subscription_payments");
        builder.Entity<SubscriptionPaymentRecord>().HasKey(entity => entity.Id);
        builder.Entity<SubscriptionPaymentRecord>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<SubscriptionPaymentRecord>().Property(entity => entity.SubscriptionId).IsRequired().HasMaxLength(64);
        builder.Entity<SubscriptionPaymentRecord>().Property(entity => entity.HotelId).IsRequired().HasMaxLength(64);
        builder.Entity<SubscriptionPaymentRecord>().Property(entity => entity.Plan).IsRequired().HasMaxLength(50);
        builder.Entity<SubscriptionPaymentRecord>().Property(entity => entity.Amount).HasPrecision(10, 2);
        builder.Entity<SubscriptionPaymentRecord>().Property(entity => entity.Method).IsRequired().HasMaxLength(50);
        builder.Entity<SubscriptionPaymentRecord>().Property(entity => entity.Status).IsRequired().HasMaxLength(50);

        builder.Entity<SubscriptionPaymentRecord>()
            .HasOne<Subscription>()
            .WithMany()
            .HasForeignKey(entity => entity.SubscriptionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SubscriptionPaymentRecord>()
            .HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(entity => entity.HotelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
