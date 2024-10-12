using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo1.Blog.Domain.DTO;

public class CommentDTO
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(1000, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres ", MinimumLength = 10)]
    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Guid PostId { get; set; }

    public Guid AuthorId { get; set; }
    public string UserName { get; set; }
}