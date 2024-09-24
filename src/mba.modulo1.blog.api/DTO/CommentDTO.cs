using mba.modulo1.blog.domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo1.Blog.API.DTO;

public class CommentDTO
{
    public Guid? Id { get; set; }
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string? Content { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Guid PostId { get; set; }

    public Guid AuthorId { get; set; }

}