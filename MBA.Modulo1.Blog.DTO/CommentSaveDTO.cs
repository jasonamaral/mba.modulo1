using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo1.Blog.DTO;

public class CommentSaveDTO
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid PostId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Content { get; set; }
}