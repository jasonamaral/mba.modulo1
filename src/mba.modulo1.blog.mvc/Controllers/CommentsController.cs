using AutoMapper;
using mba.modulo1.blog.domain.Entities;
using mba.modulo1.blog.mvc.Controllers;
using MBA.Modulo1.Blog.Data.Context;
using MBA.Modulo1.Blog.Data.Repository;
using MBA.Modulo1.Blog.Domain.Interfaces;
using MBA.Modulo1.Blog.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;

namespace MBA.Modulo1.Blog.MVC.Controllers;

[Authorize]
public class CommentsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogDbContext _context;
    private readonly IPostRepository _postRepository;
    private readonly ICommnetRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentsController(ILogger<HomeController> logger,
        BlogDbContext context,
        IPostRepository postRepository,
        ICommnetRepository commentRepository,
    IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id)
    {
        var post = _mapper.Map<PostDTO>(await _postRepository.GetByIdAsync(id));
        if (post == null)
        {
            return NotFound();
        }
        IsUserAuthenticated();
        return View(post);
    }


    [Authorize(Roles = "Admin,User")]
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
        return View();

    }

    public async Task<IActionResult> EditComment(Guid id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        // Ensure the current user is the comment's author
        if (comment.AuthorId != GetLoggedUser())
        {
            return Forbid(); // Return 403 Forbidden if the user is not the author
        }
        var commentDTO = _mapper.Map<CommentUpdateDTO>(comment);
        return View("Edit", commentDTO);
    }

    // POST: Blog/EditComment/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditComment(Guid id, CommentUpdateDTO updatedComment)
    {

        var existingComment = await _context.Comments.FindAsync(id);
        if (existingComment == null)
        {
            return NotFound();
        }

        // Ensure the current user is the comment's author
        if (existingComment.AuthorId != GetLoggedUser())
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            // Update the comment's content
            existingComment.Content = updatedComment.Content;

            try
            {
                _context.Comments
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500); // Handle concurrency errors
            }

            return RedirectToAction("Details", new { id = existingComment.PostId });
        }

        return View(existingComment);
    }

    private void IsUserAuthenticated()
    {
        TempData["IsAuthenticated"] = User?.Identity?.IsAuthenticated;
        TempData["userId"] = GetLoggedUser();
        
        //return User?.Identity?.IsAuthenticated == true;
    }
    protected string GetLoggedUser()
    {
        return User!.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }


}