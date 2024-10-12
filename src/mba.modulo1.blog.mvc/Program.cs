using mba.modulo1.blog.mvc.Data;
using MBA.Modulo1.Blog.Data.Context;
using MBA.Modulo1.Blog.Data.EntityConfig;
using MBA.Modulo1.Blog.Data.Repository;
using MBA.Modulo1.Blog.Domain.Interfaces;
using MBA.Modulo1.Blog.Domain.Notifications;
using MBA.Modulo1.Blog.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.AddDatabseSelector();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddScoped<BlogDbContext>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommnetRepository, CommentRepository>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<INotifier, Notifier>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseDbMigrationHelper();

app.Run();