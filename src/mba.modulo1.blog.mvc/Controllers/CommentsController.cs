using AutoMapper;
using mba.modulo1.blog.domain.Entities;
using mba.modulo1.blog.mvc.Controllers;
using MBA.Modulo1.Blog.Data.Context;
using MBA.Modulo1.Blog.Domain.DTO;
using MBA.Modulo1.Blog.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MBA.Modulo1.Blog.MVC.Controllers;

[Authorize]
public class CommentsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogDbContext _context;
    private readonly IPostRepository _postRepository;
    private readonly ICommnetRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly ICommentService _commentService;

    public CommentsController(ILogger<HomeController> logger,
        BlogDbContext context,
        IPostRepository postRepository,
        ICommnetRepository commentRepository,
        IMapper mapper,
        ICommentService commentService)
    {
        _logger = logger;
        _context = context;
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _mapper = mapper;
        _commentService = commentService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id)
    {
        var post = _mapper.Map<PostDTO>(await _postRepository.GetByIdAsync(id));
        if (post == null)
        {
            return View("NotFound");
        }
        LoadTempData();
        return View(post);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(CommentSaveDTO commentDTO)
    {
        if (ModelState.IsValid)
        {
            var comment = _mapper.Map<Comment>(commentDTO);
            comment.Id = Guid.NewGuid();  // Ensure a new ID is generated
            comment.AuthorId = GetLoggedUser();
            await _commentRepository.AddAsync(comment);
            return RedirectToAction("Details", new { id = comment.PostId });
        }
        LoadTempData();
        var post = _mapper.Map<PostDTO>(await _postRepository.GetByIdAsync(commentDTO.PostId));
        return View("Details", post);
    }

    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> EditComment(Guid id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return View("NotFound");
        }

        if (comment.AuthorId != GetLoggedUser())
        {
            return Forbid();
        }
        var commentDTO = _mapper.Map<CommentUpdateDTO>(comment);
        return View("Edit", commentDTO);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditComment(Guid id, CommentUpdateDTO updatedComment)
    {
        var existingComment = await _context.Comments.FindAsync(id);
        if (existingComment == null)
        {
            return View("NotFound");
        }

        if (existingComment.AuthorId != GetLoggedUser())
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            existingComment.Content = updatedComment.Content;

            try
            {
                await _commentService.UpdateAsync(_mapper.Map<Comment>(existingComment));
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }

            return RedirectToAction("Details", new { id = existingComment.PostId });
        }

        return View(existingComment);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        var existingComment = await _context.Comments.FindAsync(id);

        if (existingComment == null) return View("NotFound");
        if (existingComment.AuthorId != GetLoggedUser())
        {
            return Forbid();
        }

        await _commentService.DeleteAsync(id);

        return RedirectToAction("Details", new { id = existingComment.PostId });
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
}