using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBA.Modulo1.Blog.MVC.Controllers;

[Authorize]
public abstract class MainController: ControllerBase
    {
    }
