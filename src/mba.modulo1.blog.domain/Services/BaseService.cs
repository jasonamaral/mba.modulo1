using FluentValidation;
using FluentValidation.Results;
using MBA.Modulo1.Blog.Domain.Entities;
using MBA.Modulo1.Blog.Domain.Interfaces;
using MBA.Modulo1.Blog.Domain.Notifications;

namespace MBA.Modulo1.Blog.Domain.Services;

public abstract class BaseService
{
    private readonly INotifier _notifier;

    protected BaseService(INotifier notifier)
    {
        _notifier = notifier;
    }

    protected void Notify(ValidationResult validationResult)
    {
        foreach (var item in validationResult.Errors)
        {
            Notify(item.ErrorMessage);
        }
    }

    protected void Notify(string msg) 
    {
        _notifier.Handle(new Notification(msg));
    }

    protected bool Validate<TV, TE>(TV validate, TE entity) where TV : AbstractValidator<TE> where TE : Entity
    {
        var validator = validate.Validate(entity);
        if (validator.IsValid) return true;

        Notify(validator);
        return false;
    }
}