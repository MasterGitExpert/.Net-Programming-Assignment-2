using DeliveryLogisticsAndTracking.Components;
using DeliveryLogisticsAndTracking.Data;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

namespace DeliveryLogisticsAndTracking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContextFactory<DeliveryLogisticsAndTrackingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DeliveryLogisticsAndTrackingContext") ?? throw new InvalidOperationException("Connection string 'DeliveryLogisticsAndTrackingContext' not found.")));

            builder.Services.AddQuickGridEntityFrameworkAdapter();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddSyncfusionBlazor();

            var app = builder.Build();

            // Synfusion licensing (ideally should be in env var)
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JFaF1cX2hIfkx0Q3xbf1x1ZFREalxUTnVeUj0eQnxTdEBiWX5XcXZVQ2NdWE1/WkleYg==");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseMigrationsEndPoint();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
