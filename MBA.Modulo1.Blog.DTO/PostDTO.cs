using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo1.Blog.DTO;

public class PostDTO
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid AuthorId { get; set; }
    public string UserName { get; set; }

    public IEnumerable<CommentDTO>? Comments { get; set; }
}