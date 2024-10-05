using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo1.Blog.DTO;

public class CommentSaveDTO
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid PostId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(1000, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres ", MinimumLength = 10)]
    public string Content { get; set; }
}