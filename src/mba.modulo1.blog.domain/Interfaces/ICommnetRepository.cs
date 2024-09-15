using mba.modulo1.blog.domain.Entities;

namespace MBA.Modulo1.Blog.Domain.Interfaces;

public interface ICommnetRepository : IRepository<Comment>
{
    Task<IEnumerable<Comment>> GetCommentsByPostAsync(Guid postId);
}