using Microsoft.AspNetCore.Identity;

namespace mba.modulo1.blog.domain.Entities;

public class ApplicationUser : IdentityUser
{
    public ICollection<Post>? Posts { get; set; }

    public ICollection<Comment>? Comments { get; set; }
}