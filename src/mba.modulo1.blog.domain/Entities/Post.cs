using MBA.Modulo1.Blog.Domain.Entities;

namespace mba.modulo1.blog.domain.Entities;

public class Post : Entity
{
    public string Title { get; set; }        // Título do post
    public string Content { get; set; }      // Conteúdo do post
    public DateTime CreatedAt { get; set; }  // Data de criação do post
    public DateTime UpdatedAt { get; set; }  // Data da última atualização do post

    public Guid AuthorId { get; set; }     // Chave estrangeira para ApplicationUser

    // EF
    // public ApplicationUser Author { get; set; }

    // Relação com os comentários do post
    public ICollection<Comment> Comments { get; set; }
}