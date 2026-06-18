using Cortex.Mediator.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Senit.Platform.API.Shared.Domain.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Senit.Platform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Senit.Platform.API.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using Senit.Platform.API.Shared.Interfaces.Rest.ProblemDetails;
using Senit.Platform.API.Resources.Errors;
using Senit.Platform.API.Resources.Shared;
using Senit.Platform.API.FrontDesk.Application.CommandServices;
using Senit.Platform.API.FrontDesk.Application.Internal.CommandServices;
using Senit.Platform.API.FrontDesk.Application.Internal.QueryServices;
using Senit.Platform.API.FrontDesk.Application.QueryServices;
using Senit.Platform.API.FrontDesk.Domain.Repositories;
using Senit.Platform.API.FrontDesk.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Senit.Platform.API.FrontDesk.Resources;
using Senit.Platform.API.Iam.Application.CommandServices;
using Senit.Platform.API.Iam.Application.Internal.CommandServices;
using Senit.Platform.API.Iam.Application.Internal.QueryServices;
using Senit.Platform.API.Iam.Application.QueryServices;
using Senit.Platform.API.Iam.Domain.Repositories;
using Senit.Platform.API.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Senit.Platform.API.Iam.Resources;
using Senit.Platform.API.Room.Application.CommandServices;
using Senit.Platform.API.Room.Application.Internal.CommandServices;
using Senit.Platform.API.Room.Application.Internal.QueryServices;
using Senit.Platform.API.Room.Application.QueryServices;
using Senit.Platform.API.Room.Domain.Repositories;
using Senit.Platform.API.Room.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Senit.Platform.API.Room.Resources;
using Senit.Platform.API.Reservation.Application.CommandServices;
using Senit.Platform.API.Reservation.Application.Internal.CommandServices;
using Senit.Platform.API.Reservation.Application.Internal.QueryServices;
using Senit.Platform.API.Reservation.Application.QueryServices;
using Senit.Platform.API.Reservation.Domain.Repositories;
using Senit.Platform.API.Reservation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Senit.Platform.API.Reservation.Resources;
using Senit.Platform.API.GuestStay.Application.CommandServices;
using Senit.Platform.API.GuestStay.Application.Internal.CommandServices;
using Senit.Platform.API.GuestStay.Application.Internal.QueryServices;
using Senit.Platform.API.GuestStay.Application.QueryServices;
using Senit.Platform.API.GuestStay.Domain.Repositories;
using Senit.Platform.API.GuestStay.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Senit.Platform.API.GuestStay.Resources;
using Senit.Platform.API.Payment.Application.CommandServices;
using Senit.Platform.API.Payment.Application.Internal.CommandServices;
using Senit.Platform.API.Payment.Application.Internal.QueryServices;
using Senit.Platform.API.Payment.Application.QueryServices;
using Senit.Platform.API.Payment.Domain.Repositories;
using Senit.Platform.API.Payment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Senit.Platform.API.Payment.Resources;
using Senit.Platform.API.Housekeeping.Application.CommandServices;
using Senit.Platform.API.Housekeeping.Application.Internal.CommandServices;
using Senit.Platform.API.Housekeeping.Application.Internal.QueryServices;
using Senit.Platform.API.Housekeeping.Application.QueryServices;
using Senit.Platform.API.Housekeeping.Domain.Repositories;
using Senit.Platform.API.Housekeeping.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Senit.Platform.API.Housekeeping.Resources;
using Senit.Platform.API.SubscriptionPayment.Application.CommandServices;
using Senit.Platform.API.SubscriptionPayment.Application.Internal.CommandServices;
using Senit.Platform.API.SubscriptionPayment.Application.Internal.QueryServices;
using Senit.Platform.API.SubscriptionPayment.Application.QueryServices;
using Senit.Platform.API.SubscriptionPayment.Domain.Repositories;
using Senit.Platform.API.SubscriptionPayment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Senit.Platform.API.SubscriptionPayment.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

builder.Services.AddLocalization(options => options.ResourcesPath = "");
builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services.AddSingleton<IStringLocalizer<CommonMessages>, StringLocalizer<CommonMessages>>();
builder.Services.AddSingleton<IStringLocalizer<FrontDeskMessages>, StringLocalizer<FrontDeskMessages>>();
builder.Services.AddSingleton<IStringLocalizer<IamMessages>, StringLocalizer<IamMessages>>();
builder.Services.AddSingleton<IStringLocalizer<RoomMessages>, StringLocalizer<RoomMessages>>();
builder.Services.AddSingleton<IStringLocalizer<ReservationMessages>, StringLocalizer<ReservationMessages>>();
builder.Services.AddSingleton<IStringLocalizer<GuestStayMessages>, StringLocalizer<GuestStayMessages>>();
builder.Services.AddSingleton<IStringLocalizer<PaymentMessages>, StringLocalizer<PaymentMessages>>();
builder.Services.AddSingleton<IStringLocalizer<HousekeepingMessages>, StringLocalizer<HousekeepingMessages>>();
builder.Services.AddSingleton<IStringLocalizer<SubscriptionPaymentMessages>, StringLocalizer<SubscriptionPaymentMessages>>();

builder.Services.AddSingleton<ProblemDetailsFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IHotelCommandService, HotelCommandService>();
builder.Services.AddScoped<IHotelQueryService, HotelQueryService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHotelStaffMemberRepository, HotelStaffMemberRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomCommandService, RoomCommandService>();
builder.Services.AddScoped<IRoomQueryService, RoomQueryService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationCommandService, ReservationCommandService>();
builder.Services.AddScoped<IReservationQueryService, ReservationQueryService>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IGuestCommandService, GuestCommandService>();
builder.Services.AddScoped<IGuestQueryService, GuestQueryService>();
builder.Services.AddScoped<IGuestStayRepository, GuestStayRepository>();
builder.Services.AddScoped<IGuestStayCommandService, GuestStayCommandService>();
builder.Services.AddScoped<IGuestStayQueryService, GuestStayQueryService>();
builder.Services.AddScoped<IConsumptionRepository, ConsumptionRepository>();
builder.Services.AddScoped<IConsumptionCommandService, ConsumptionCommandService>();
builder.Services.AddScoped<IConsumptionQueryService, ConsumptionQueryService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentCommandService, PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService, PaymentQueryService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceCommandService, InvoiceCommandService>();
builder.Services.AddScoped<IInvoiceQueryService, InvoiceQueryService>();
builder.Services.AddScoped<ICleaningTaskRepository, CleaningTaskRepository>();
builder.Services.AddScoped<ICleaningTaskCommandService, CleaningTaskCommandService>();
builder.Services.AddScoped<ICleaningTaskQueryService, CleaningTaskQueryService>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();
builder.Services.AddScoped<ISubscriptionPaymentRepository, SubscriptionPaymentRepository>();
builder.Services.AddScoped<ISubscriptionPaymentCommandService, SubscriptionPaymentCommandService>();
builder.Services.AddScoped<ISubscriptionPaymentQueryService, SubscriptionPaymentQueryService>();
builder.Services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();

builder.Services.AddCortexMediator([typeof(Program)]);

var app = builder.Build();

// Create the database schema automatically for local and simple deployment scenarios.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.UseGlobalExceptionHandler();

var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAllPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
