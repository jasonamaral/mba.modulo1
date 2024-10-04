using AutoMapper;
using mba.modulo1.blog.domain.Entities;
using mba.modulo1.blog.mvc.Models;
using MBA.Modulo1.Blog.Data.Context;
using MBA.Modulo1.Blog.Domain.Interfaces;
using MBA.Modulo1.Blog.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

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
        LoadTempData();
        var posts = _mapper.Map<IEnumerable<PostDTO>>(await _postRepository.GetAllAsync());
        return View(posts.OrderByDescending(j => j.CreatedAt));
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddPost(PostSaveDTO postDTO)
    {
        if (ModelState.IsValid)
        {
            var post = _mapper.Map<Post>(postDTO);
            post.Id = Guid.NewGuid();  // Ensure a new ID is generated
            post.AuthorId = GetLoggedUser();
            await _postRepository.AddAsync(post);
        }
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPost(Guid id, PostSaveDTO updatedPost)
    {
        var existingPost = await _context.Posts.FindAsync(id);
        if (existingPost == null)
        {
            return View("NotFound");
        }

        if (existingPost.AuthorId != GetLoggedUser())
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            existingPost.Title = updatedPost.Title;
            existingPost.Content = updatedPost.Content;

            try
            {
                await _postRepository.UpdateAsync(_mapper.Map<Post>(existingPost));
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }

            return RedirectToAction("Index", new { id = existingPost.Id });
        }

        return View(existingPost);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var existingPost = await _context.Posts.FindAsync(id);

        if (existingPost == null) return View("NotFound");
        if (existingPost.AuthorId != GetLoggedUser())
        {
            return Forbid();
        }

        await _postRepository.DeleteByIdAsync(id);

        return RedirectToAction("Index");
    }

    private void LoadTempData()
    {
        TempData["userId"] = GetLoggedUser();
        TempData["IsAuthenticated"] = User?.Identity?.IsAuthenticated;

        if (User?.Identity?.IsAuthenticated == true)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            TempData["IsAdmin"] = role == "Admin";
        }
        else
        {
            TempData["IsAdmin"] = false;
        }
    }

    protected string GetLoggedUser()
    {
        return User!.FindFirstValue(ClaimTypes.NameIdentifier)!;
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