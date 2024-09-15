using mba.modulo1.blog.domain.Entities;
using MBA.Modulo1.Blog.Data.Context;
using MBA.Modulo1.Blog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo1.Blog.Data.Repository;

public class CommentRepository : Repository<Comment>, ICommnetRepository
{
    public CommentRepository(BlogDbContext dbContext) : base(dbContext)
    { }

    public async Task<IEnumerable<Comment>> GetCommentsByPostAsync(Guid postId)
    {
        return await Db.Comments.AsNoTracking().Where(j => j.PostId == postId).ToListAsync();
    }
}