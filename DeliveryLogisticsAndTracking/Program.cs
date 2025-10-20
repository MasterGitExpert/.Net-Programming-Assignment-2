using DeliveryLogisticsAndTracking.Components;
using DotNetEnv;
using Radzen;

namespace DeliveryLogisticsAndTracking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddRadzenComponents();
            builder.Services.AddGeolocationServices();
            builder.Configuration.AddUserSecrets<Program>();
            var config = builder.Configuration;

            Env.Load();
            builder.Configuration.AddEnvironmentVariables();

            // Access the environment variables
            string apiKey = Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY");

            var app = builder.Build();

            app.MapGet("/", () => $"API Key: {apiKey}");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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
