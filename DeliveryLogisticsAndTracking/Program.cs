using DeliveryLogisticsAndTracking.Components;
using DotNetEnv;
using Radzen;
using DeliveryLogisticsAndTracking.Data;
using DeliveryLogisticsAndTracking.Models;
using DeliveryLogisticsAndTracking.Services;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

namespace DeliveryLogisticsAndTracking
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // -----------------------------
            // Services
            // -----------------------------
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Single merged DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DeliveryLogisticsAndTrackingContext")));

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<SessionStateService>();

            // Syncfusion
            builder.Services.AddSyncfusionBlazor();
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

            // -----------------------------
            // DB initialization and default admin
            // -----------------------------
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userService = scope.ServiceProvider.GetRequiredService<UserService>();

                // Apply migrations
                db.Database.Migrate();

                // Seed default admin if none exists
                if (!await db.Users.AnyAsync())
                {
                    var admin = new User
                    {
                        Name = "Administrator",
                        UserType = "Admin", // ensure User class has this property
                        Email = "admin@example.com",
                        DOB = DateTime.Today,
                        Phone = "0000000000",
                        Address = "Admin HQ"
                    };

                    using var transaction = await db.Database.BeginTransactionAsync();
                    try
                    {
                        db.Users.Add(admin);
                        await db.SaveChangesAsync();

                        await userService.AddUserPasswordAsync(
                            admin.UserId,
                            userService.HashPassword("Admin123!")
                        );

                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }

            // -----------------------------
            // Middleware pipeline
            // -----------------------------
            app.MapGet("/", () => $"API Key: {apiKey}");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            await app.RunAsync();
        }
    }
}