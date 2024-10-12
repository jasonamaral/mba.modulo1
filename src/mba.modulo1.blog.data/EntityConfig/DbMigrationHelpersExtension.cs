using mba.modulo1.blog.domain.Entities;
using MBA.Modulo1.Blog.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MBA.Modulo1.Blog.Data.EntityConfig;

public static class DbMigrationHelpersExtension
{
    public static void UseDbMigrationHelper(this WebApplication app)
    {
        DbMigrationHelpers.EnsureSeedData(app).Wait();
    }
}

public static class DbMigrationHelpers
{
    public static async Task EnsureSeedData(WebApplication serviceScope)
    {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
        await EnsureSeedDataAsync(services);
    }

    public static async Task EnsureSeedDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var context = scope.ServiceProvider.GetRequiredService<BlogDbContext>();

        if (env.IsDevelopment() || env.IsStaging())
        {
            await context.Database.MigrateAsync();
            await EnsureSeedAdmin(context);
        }
    }

    private static async Task EnsureSeedAdmin(BlogDbContext context)
    {
        if (context.Users.Any())
        {
            return;
        }

        var adminRole = new IdentityRole
        {
            Id = Guid.NewGuid().ToString(),
            Name = "User",
            NormalizedName = "USER"
        };
        await context.Roles.AddAsync(adminRole);

        adminRole = new IdentityRole
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Admin",
            NormalizedName = "ADMIN"
        };
        await context.Roles.AddAsync(adminRole);

        await context.SaveChangesAsync();

        var adminEmail = "admin@example.com";
        var adminPassword = "Teste@123";
        var passwordHasher = new PasswordHasher<IdentityUser>();

        var adminUser = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = adminEmail,
            NormalizedUserName = adminEmail.ToUpper(),
            Email = adminEmail,
            NormalizedEmail = adminEmail.ToUpper(),
            EmailConfirmed = true,
            AccessFailedCount = 0,
            LockoutEnabled = true,
            TwoFactorEnabled = false,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString()
        };
        string hashedPassword = passwordHasher.HashPassword(adminUser, adminPassword);
        adminUser.PasswordHash = hashedPassword;

        await context.Users.AddAsync(adminUser);

        var userRole = new IdentityUserRole<string>
        {
            UserId = adminUser.Id,
            RoleId = adminRole.Id
        };
        await context.UserRoles.AddAsync(userRole);

        context.SaveChanges();
    }
}