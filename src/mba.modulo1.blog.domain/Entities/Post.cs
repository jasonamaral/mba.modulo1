using MBA.Modulo1.Blog.Domain.Entities;

namespace mba.modulo1.blog.domain.Entities;

public class Post : Entity
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string AuthorId { get; set; }

    // EF
    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<Comment>? Comments { get; set; }
}