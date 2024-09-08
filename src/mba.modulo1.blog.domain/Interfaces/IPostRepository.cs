using mba.modulo1.blog.domain.Entities;

namespace MBA.Modulo1.Blog.Domain.Interfaces;

public interface IPostRepository : IRepository<Post>
{
    Task<IEnumerable<Post>> GetPostsByAuthorAsync(Guid author);
    Task<IEnumerable<Comment>> GetCommentsByPostAsync(Guid postId);
}