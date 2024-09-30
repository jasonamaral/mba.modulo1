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

    public override async Task<Post> GetByIdAsync(Guid id)
        => await Db.Posts.AsNoTracking()
                .Include(post => post.User) // Include User for the Post itself
                .Include(post => post.Comments) // Include Comments for the Post
                    .ThenInclude(comment => comment.User) // Include User for each Comment
                .Where(post => post.Id == id)
                .Select(post => new Post
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    User = post.User, // User of the post
                    Comments = post.Comments
                                 .OrderByDescending(c => c.CreatedAt)
                                 .ToList() // Ensure comments are ordered within the projection
                })
                .FirstOrDefaultAsync();

    public override async Task<List<Post>> GetAllAsync()
    {
        //return await DbSet.AsNoTracking().ToListAsync();

        return await Db.Posts.AsNoTracking()
        .Include(j => j.User).ToListAsync();
    }
}