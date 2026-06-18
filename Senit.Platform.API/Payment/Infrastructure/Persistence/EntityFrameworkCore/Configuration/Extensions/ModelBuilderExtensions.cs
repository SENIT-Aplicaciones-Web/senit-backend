using Microsoft.EntityFrameworkCore;
using Senit.Platform.API.FrontDesk.Domain.Model.Aggregates;
using Senit.Platform.API.GuestStay.Domain.Model.Aggregates;
using Senit.Platform.API.Payment.Domain.Model.Aggregates;
using Senit.Platform.API.Reservation.Domain.Model.Aggregates;

namespace Senit.Platform.API.Payment.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Payment bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Payment bounded context database configuration.
    /// </summary>
    /// <param name="builder">
    ///     The model builder used to configure the persistence model.
    /// </param>
    public static void ApplyPaymentConfiguration(this ModelBuilder builder)
    {
        builder.Entity<PaymentRecord>().ToTable("payments");
        builder.Entity<PaymentRecord>().HasKey(entity => entity.Id);
        builder.Entity<PaymentRecord>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<PaymentRecord>().Property(entity => entity.HotelId).IsRequired().HasMaxLength(64);
        builder.Entity<PaymentRecord>().Property(entity => entity.GuestStayId).HasMaxLength(64);
        builder.Entity<PaymentRecord>().Property(entity => entity.ReservationId).HasMaxLength(64);
        builder.Entity<PaymentRecord>().Property(entity => entity.Amount).HasPrecision(10, 2);
        builder.Entity<PaymentRecord>().Property(entity => entity.Method).IsRequired().HasMaxLength(50);
        builder.Entity<PaymentRecord>().Property(entity => entity.Status).IsRequired().HasMaxLength(50);

        builder.Entity<PaymentRecord>()
            .HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(entity => entity.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PaymentRecord>()
            .HasOne<GuestStayRecord>()
            .WithMany()
            .HasForeignKey(entity => entity.GuestStayId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PaymentRecord>()
            .HasOne<HotelReservation>()
            .WithMany()
            .HasForeignKey(entity => entity.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Invoice>().ToTable("invoices");
        builder.Entity<Invoice>().HasKey(entity => entity.Id);
        builder.Entity<Invoice>().Property(entity => entity.Id).IsRequired().HasMaxLength(64);
        builder.Entity<Invoice>().Property(entity => entity.PaymentId).IsRequired().HasMaxLength(64);
        builder.Entity<Invoice>().Property(entity => entity.Number).IsRequired().HasMaxLength(50);
        builder.Entity<Invoice>().Property(entity => entity.CustomerName).IsRequired().HasMaxLength(250);
        builder.Entity<Invoice>().Property(entity => entity.Amount).HasPrecision(10, 2);

        builder.Entity<Invoice>()
            .HasOne<PaymentRecord>()
            .WithMany()
            .HasForeignKey(entity => entity.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
