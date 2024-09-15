using MBA.Modulo1.Blog.Data.Context;
using MBA.Modulo1.Blog.Data.Repository;
using MBA.Modulo1.Blog.Domain.Interfaces;
using MBA.Modulo1.Blog.Domain.Notifications;
using MBA.Modulo1.Blog.Domain.Services;

namespace MBA.Modulo1.Blog.API.Config;

public static class IoCConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<BlogDbContext>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommnetRepository, CommentRepository>();

        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<INotifier, Notifier>();

        return services;
    }
}