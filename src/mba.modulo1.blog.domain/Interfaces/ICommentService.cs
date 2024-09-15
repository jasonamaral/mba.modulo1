using mba.modulo1.blog.domain.Entities;

namespace MBA.Modulo1.Blog.Domain.Interfaces;

public interface ICommentService : IDisposable
{
    Task AddAsync(Comment comment);

    Task UpdateAsync(Comment comment);

    Task DeleteAsync(Guid id);
}