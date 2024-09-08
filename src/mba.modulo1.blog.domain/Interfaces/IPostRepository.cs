using mba.modulo1.blog.domain.Entities;

namespace MBA.Modulo1.Blog.Domain.Interfaces;

public interface IPostRepository : IRepository<Post>
{
    Task<IEnumerable<Post>> GetPostsByAuthor(Guid author);
    Task<IEnumerable<Comment>> GetCommentsByPost(Guid postId);
}