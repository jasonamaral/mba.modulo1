using mba.modulo1.blog.domain.Entities;

namespace MBA.Modulo1.Blog.Domain.Interfaces;

public interface IPostService : IDisposable
{
    Task AddAsync(Post post);

    Task UpdateAsync(Post post);

    Task DeleteAsync(Guid id);
}