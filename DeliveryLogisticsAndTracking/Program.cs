using DeliveryLogisticsAndTracking.Components;
using DeliveryLogisticsAndTracking.Data;
using DeliveryLogisticsAndTracking.Models;
using DeliveryLogisticsAndTracking.Services;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor; // <-- Add this

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Services
// -----------------------------
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// -----------------------------
// Syncfusion
// -----------------------------
builder.Services.AddSyncfusionBlazor(); // <-- Add this

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SessionStateService>();

var app = builder.Build();

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

// -----------------------------
// Middleware pipeline
// -----------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();

// Correct fallback to root Pages/_Host.cshtml
app.MapFallbackToPage("/_Host");

app.Run();