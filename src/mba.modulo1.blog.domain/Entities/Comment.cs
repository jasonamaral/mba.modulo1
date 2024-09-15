using MBA.Modulo1.Blog.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace mba.modulo1.blog.domain.Entities;

public class Comment : Entity
{
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Guid PostId { get; set; }

    public string AuthorId { get; set; }

    //EF
    public virtual ApplicationUser User { get; set; }

    public virtual Post Post { get; set; }
}