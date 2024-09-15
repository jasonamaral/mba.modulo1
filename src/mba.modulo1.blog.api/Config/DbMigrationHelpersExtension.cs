using MBA.Modulo1.Blog.API.Data;
using MBA.Modulo1.Blog.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo1.Blog.API.Config;

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
        var contextId = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (env.IsDevelopment())
        {
            await context.Database.MigrateAsync();
            await contextId.Database.MigrateAsync();

            await EnsureSeedAdmin(contextId);
        }
        
    }

    private static async Task EnsureSeedAdmin(ApplicationDbContext context)
    {
        if (context.Users.Any())
        {
            return;
        }

        //var userManager = context.GetService<UserManager<IdentityUser>>();
        //var roleManager = context.GetService<RoleManager<IdentityRole>>();

        //await roleManager.CreateAsync(new IdentityRole("admin"));
        //await roleManager.CreateAsync(new IdentityRole("user"));

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

        // Admin user details
        var adminEmail = "admin@admin.com";
        var adminPassword = "Teste123";
        var passwordHasher = new PasswordHasher<IdentityUser>();

        // Create the admin user
        var adminUser = new IdentityUser
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

        //await context.Roles.AddAsync(new IdentityRole("Admin"));
        //await context.Roles.AddAsync(new IdentityRole("User"));
        //await context.UserRoles.AddAsync(new IdentityUserRole<string>();

        await context.Users.AddAsync(adminUser);

        var userRole = new IdentityUserRole<string>
        {
            UserId = adminUser.Id,
            RoleId = adminRole.Id
        };
        await context.UserRoles.AddAsync(userRole);

        //var result = await userManager.CreateAsync(adminUser, adminPassword);
        //if (result.Succeeded)
        //{
        // Add the admin user to the Admin role
        //await userManager.AddToRoleAsync(adminUser, "admin");
        //}
        context.SaveChanges();
    }
}
