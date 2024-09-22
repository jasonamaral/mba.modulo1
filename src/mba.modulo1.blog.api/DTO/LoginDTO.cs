using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo1.Blog.API.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O {0} é inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres ", MinimumLength = 8)]
    public string Password { get; set; }
}