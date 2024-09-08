using FluentValidation;
using mba.modulo1.blog.domain.Entities;

namespace MBA.Modulo1.Blog.Domain.Entities.Validations;

public class CommentValidation : AbstractValidator<Comment>
{
    public CommentValidation()
    {
        RuleFor(j => j.AuthorId).NotEmpty().WithMessage("O campo {PropertyName} precisa ser preechido");
        RuleFor(j => j.Content)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preechido")
            .Length(2, 500).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLenght} caracteres");

        RuleFor(j => j.PostId).NotEmpty().WithMessage("O campo {PropertyName} precisa ser preechido");

    }
}