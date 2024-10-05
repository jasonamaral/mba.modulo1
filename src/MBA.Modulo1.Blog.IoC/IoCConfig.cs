using MBA.Modulo1.Blog.Data.Context;
using MBA.Modulo1.Blog.Data.Repository;
using MBA.Modulo1.Blog.Domain.Interfaces;
using MBA.Modulo1.Blog.Domain.Notifications;
using MBA.Modulo1.Blog.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MBA.Modulo1.Blog.IoC;

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