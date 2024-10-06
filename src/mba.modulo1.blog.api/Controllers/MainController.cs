using MBA.Modulo1.Blog.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Security.Claims;

namespace MBA.Modulo1.Blog.API.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    private readonly INotifier _notifier;

    protected MainController(INotifier notifier)
    {
        _notifier = notifier;
    }

    protected bool IsValid()
    {
        return !_notifier.HasNotification();
    }

    protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null)
    {
        if (IsValid())
        {
            return new ObjectResult(result) { StatusCode = Convert.ToInt32(statusCode) };
        }

        return BadRequest(new
        {
            errors = _notifier.GetNotifications().Select(j => j.Message)
        });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyInvalidModel(modelState);

        return CustomResponse();
    }

    protected void NotifyInvalidModel(ModelStateDictionary modelstate)
    {
        var errors = modelstate.Values.SelectMany(j => j.Errors);
        foreach (var error in errors)
        {
            var msg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
            NotifyError(msg);
        }
    }

    protected void NotifyError(string msg)
    {
        _notifier.Handle(new Domain.Notifications.Notification(msg));
    }

    protected string GetLoggedUser()
    {
        return User!.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }

    protected bool UserHasPermition(Guid userId)
    {
        var role = User.FindFirstValue(ClaimTypes.Role);

        if (role == "Admin") return true;

        if (userId.ToString() == GetLoggedUser()) return true;

        return false;
    }
}