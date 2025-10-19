using DeliveryLogisticsAndTracking.Components;
using DeliveryLogisticsAndTracking.Data;
using DeliveryLogisticsAndTracking.Models;
using DeliveryLogisticsAndTracking.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;


// var builder = WebApplication.CreateBuilder(args);

// // -----------------------------
// // Services
// // -----------------------------
// builder.Services.AddRazorPages();
// builder.Services.AddServerSideBlazor();

// // -----------------------------
// // Syncfusion
// // -----------------------------
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddScoped<UserService>();
// builder.Services.AddScoped<SessionStateService>();

// var app = builder.Build();

// // -----------------------------
// // DB initialization and default admin
// // -----------------------------
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     var userService = scope.ServiceProvider.GetRequiredService<UserService>();

//     db.Database.Migrate();

//     if (!await db.Users.AnyAsync())
//     {
//         var admin = new User
//         {
//             Name = "Administrator",
//             UserType = "Admin",
//             Email = "admin@example.com",
//             DOB = DateTime.Today,
//             Phone = "0000000000",
//             Address = "Admin HQ"
//         };

//         using var transaction = await db.Database.BeginTransactionAsync();
//         try
//         {
//             db.Users.Add(admin);
//             await db.SaveChangesAsync();

//             await userService.AddUserPasswordAsync(
//                 admin.UserId,
//                 userService.HashPassword("Admin123!")
//             );

//             await transaction.CommitAsync();
//         }
//         catch
//         {
//             await transaction.RollbackAsync();
//             throw;
//         }
//     }
// }

// // -----------------------------
// // Middleware pipeline
// // -----------------------------
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     app.UseHsts();
// }


namespace DeliveryLogisticsAndTracking
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // -----------------------------
            // DB initialization and default admin
            // -----------------------------
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userService = scope.ServiceProvider.GetRequiredService<UserService>();

                db.Database.Migrate();

                if (!await db.Users.AnyAsync())
                {
                    var admin = new User
                    {
                        Name = "Administrator",
                        UserType = "Admin",
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

            var builder = WebApplication.CreateBuilder(args);

            // -----------------------------
            // Services
            // -----------------------------
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContextFactory<DeliveryLogisticsAndTrackingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DeliveryLogisticsAndTrackingContext") ?? throw new InvalidOperationException("Connection string 'DeliveryLogisticsAndTrackingContext' not found.")));

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<SessionStateService>();


            builder.Services.AddQuickGridEntityFrameworkAdapter();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add Syncfusion services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddSyncfusionBlazor();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseMigrationsEndPoint();
            }
        }
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();

// Correct fallback to root Pages/_Host.cshtml
app.MapFallbackToPage("/_Host");

app.Run();

