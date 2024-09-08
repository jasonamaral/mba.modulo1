using mba.modulo1.blog.domain.Entities;
using mba.modulo1.blog.mvc.Data.Context;
using MBA.Modulo1.Blog.Domain.Interfaces;
using System.Data.Entity;

namespace MBA.Modulo1.Blog.Data.Repository;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(BlogDbContext dbContext) : base(dbContext)
    { }

    public async Task<IEnumerable<Comment>> GetCommentsByPostAsync(Guid postId)
    {
        return await Db.Comments.AsNoTracking().Where(j => j.PostId == postId).ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPostsByAuthorAsync(Guid authorId)
    {
        return await GetAsync(j => j.AuthorId == authorId);
    }
}