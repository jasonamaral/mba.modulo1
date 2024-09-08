using mba.modulo1.blog.domain.Entities;

namespace MBA.Modulo1.Blog.Domain.Interfaces;

public interface ICommentService : IDisposable
{
    Task Add(Comment comment);

    Task Update(Comment comment);

    Task Delete(Comment id);
}