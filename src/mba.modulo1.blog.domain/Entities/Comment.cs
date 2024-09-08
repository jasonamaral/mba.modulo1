using MBA.Modulo1.Blog.Domain.Entities;

namespace mba.modulo1.blog.domain.Entities;

public class Comment : Entity
{
    public string Content { get; set; }      // Conteúdo do comentário
    public DateTime CreatedAt { get; set; }  // Data de criação do comentário
    public DateTime UpdatedAt { get; set; }  // Data da última atualização do comentário

    // Relação com o Post
    public Guid PostId { get; set; }          // Chave estrangeira para o Post

    public Guid AuthorId { get; set; }     // Chave estrangeira para ApplicationUser

    // EF
   // public ApplicationUser Author { get; set; }
    public Post Post { get; set; }
}