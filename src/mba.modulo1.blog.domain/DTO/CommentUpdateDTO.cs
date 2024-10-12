using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo1.Blog.Domain.DTO;

public class CommentUpdateDTO
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(1000, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres ", MinimumLength = 10)]
    public string Content { get; set; }
}