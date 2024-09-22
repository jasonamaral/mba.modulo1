using MBA.Modulo1.Blog.Domain.Entities;

namespace mba.modulo1.blog.domain.Entities;

public class Comment : Entity
{
    public string Content { get; set; }

    public Guid PostId { get; set; }

    public string AuthorId { get; set; }

    //EF
    public virtual ApplicationUser User { get; set; }

    public virtual Post Post { get; set; }
}