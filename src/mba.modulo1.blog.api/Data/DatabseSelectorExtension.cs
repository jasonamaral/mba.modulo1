using MBA.Modulo1.Blog.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo1.Blog.API.Data;

public static class DatabseSelectorExtension
{
    public static void AddDatabseSelector(this WebApplicationBuilder builder)
    {
        

        if (builder.Environment.IsDevelopment())
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionLite") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
            builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlite(connectionString));
        }
        else
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}