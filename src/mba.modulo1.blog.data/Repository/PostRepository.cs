using mba.modulo1.blog.domain.Entities;
using MBA.Modulo1.Blog.Data.Context;
using MBA.Modulo1.Blog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo1.Blog.Data.Repository;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(BlogDbContext dbContext) : base(dbContext)
    { }

    public async Task<IEnumerable<Post>> GetPostsByAuthorAsync(string authorId)
    {
        return await Db.Posts.AsNoTracking()
        .Include(j => j.Comments)
        .Where(j => j.AuthorId == authorId)
        .OrderBy(j => j.UpdatedAt)
        .ToListAsync();
    }

    public async override Task<Post> GetByIdAsync(Guid id)
    {
        return await Db.Posts.AsNoTracking()
        .Include(j => j.Comments)
        .FirstOrDefaultAsync(j => j.Id == id);

    }
}