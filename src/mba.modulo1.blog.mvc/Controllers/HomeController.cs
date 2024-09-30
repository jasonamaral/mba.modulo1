using AutoMapper;
using mba.modulo1.blog.mvc.Models;
using MBA.Modulo1.Blog.Data.Context;
using MBA.Modulo1.Blog.Domain.Interfaces;
using MBA.Modulo1.Blog.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace mba.modulo1.blog.mvc.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogDbContext _context;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger,
        BlogDbContext blogDbContext,
        IPostRepository postRepository,
        IMapper mapper)
    {
        _logger = logger;
        _context = blogDbContext;
        _postRepository = postRepository;
        _mapper = mapper;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var posts = _mapper.Map<IEnumerable<PostDTO>>(await _postRepository.GetAllAsync());
        return View(posts.OrderByDescending(j => j.CreatedAt));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}