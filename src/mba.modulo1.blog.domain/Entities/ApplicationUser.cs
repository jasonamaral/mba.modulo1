using Microsoft.AspNet.Identity.EntityFramework;

namespace mba.modulo1.blog.domain.Entities;

public class ApplicationUser : IdentityUser
{
    // Propriedade para associar o usuário aos posts que ele criou
    public ICollection<Post>? Posts { get; set; }

    // Propriedade para associar o usuário aos comentários que ele fez
    public ICollection<Comment>? Comments { get; set; }
}