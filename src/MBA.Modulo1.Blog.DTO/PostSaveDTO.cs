using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo1.Blog.DTO;

public class PostSaveDTO
{
    [Required(ErrorMessage = "O campo título é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo título deve ter entre {2} e {1} caracteres ", MinimumLength = 10)]
    public string Title { get; set; }

    [Required(ErrorMessage = "O campo Conteúdo é obrigatório")]
    [StringLength(1000, ErrorMessage = "O campo Conteúdo deve ter entre {2} e {1} caracteres ", MinimumLength = 10)]
    public string Content { get; set; }
}