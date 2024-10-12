using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo1.Blog.Domain.DTO;

public class PostDTO
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "O campo título é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo título deve ter entre {2} e {1} caracteres ", MinimumLength = 3)]
    public string? Title { get; set; }

    [Required(ErrorMessage = "O campo Conteúdo é obrigatório")]
    [StringLength(1000, ErrorMessage = "O campo Conteúdo deve ter entre {2} e {1} caracteres ", MinimumLength =3)]
    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid AuthorId { get; set; }

    public string UserName { get; set; }

    public IEnumerable<CommentDTO>? Comments { get; set; }
}