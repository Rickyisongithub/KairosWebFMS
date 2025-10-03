using KairosWebAPI.AuthorizationFilter;
using KairosWebAPI.Services.Jobs;
using KairosWebAPI.Services.KairosService;
using KairosWebAPI.Services.Order;
using KairosWebAPI.Services.Trucks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using KairosWebAPI.DatabaseContext;
using KairosWebAPI.Services.Logs;
using KairosWebAPI.Services.ChatService;
using KairosWebAPI.Services.Customer;
using KairosWebAPI.Services.DriverLeaveService;
using KairosWebAPI.Services.FileService;
using KairosWebAPI.Services.TimeEntryService;
using KairosWebAPI.Services.TokenService;
using KairosWebAPI.Services.Vendor;
using KairosWebAPI.Services.DriverLeaveBalanceService;

namespace KairosWebAPI.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<KAuthorizationFilter>();
            services.AddScoped<IKairosService, KairosService>();
            services.AddScoped<IVendorService, VendorService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ITrucksService, TrucksService>();
            services.AddScoped<IJobsService, JobsService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IDriverLeaveService, DriverLeaveService>();
            services.AddScoped<ITimeEntryService, TimeEntryService>();
            services.AddScoped<IDriverLeaveBalanceService, DriverLeaveBalanceService>();

        }

        public static void ConfigureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnectionString"))
                .EnableSensitiveDataLogging();
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.ConfigureWarnings(builder =>
                {
                    builder.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning);
                });
            });
        }

    }
}
