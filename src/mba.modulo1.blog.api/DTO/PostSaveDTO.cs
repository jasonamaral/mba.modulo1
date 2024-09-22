using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo1.Blog.API.DTO;

public class PostSaveDTO
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Title { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Content { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid AuthorId { get; set; }
}