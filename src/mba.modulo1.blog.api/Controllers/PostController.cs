using AutoMapper;
using mba.modulo1.blog.domain.Entities;
using MBA.Modulo1.Blog.API.DTO;
using MBA.Modulo1.Blog.Domain.Interfaces;
using MBA.Modulo1.Blog.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MBA.Modulo1.Blog.API.Controllers;

[Authorize]
[Route("api/posts")]
public class PostController : MainController
{
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostController(IPostService postService, 
        ICommentService commentService, 
        IPostRepository postRepository, 
        IMapper mapper, 
        INotifier notifier) : base(notifier)
    {
        _postService = postService;
        _commentService = commentService;
        _postRepository = postRepository;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<PostDTO>> GetAll()
    {
        return _mapper.Map<IEnumerable<PostDTO>>(await _postRepository.GetAllAsync());
    }

    [AllowAnonymous]
    [HttpGet("GetById/{id:guid}")]
    public async Task<ActionResult<PostDTO>> GetByIdAsync(Guid id)
    {
        var post = await GetPostByIdAsync(id);

        if (post == null) return NotFound();

        return Ok(post);
    }

    [AllowAnonymous]
    [HttpGet("GetByAuthorId/{id:guid}")]
    public async Task<IEnumerable<PostDTO>> GetAll(string id)
    {
        return _mapper.Map<IEnumerable<PostDTO>>(await _postRepository.GetPostsByAuthorAsync(id));
    }

    [HttpPost("Add")]
    public async Task<ActionResult<PostSaveDTO>> AddAsync(PostSaveDTO postDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        var post = _mapper.Map<Post>(postDTO);
        post.AuthorId = GetLoggedUser();
        await _postRepository.AddAsync(post);
        return CustomResponse(HttpStatusCode.Created, postDTO);
    }

    [HttpPut("Update/{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, PostSaveDTO postDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var post = await GetPostByIdAsync(id);
        if (post == null) return NotFound();
        if (!UserHasPermition(post.AuthorId)) return Forbid();

        //post.UpdatedAt = DateTime.Now;
        post.Title = postDTO.Title;
        post.Content = postDTO.Content;

        await _postService.UpdateAsync(_mapper.Map<Post>(post));
        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("Delete/{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var post = await GetPostByIdAsync(id);
        if (post == null) return NotFound();
        if (!UserHasPermition(post.AuthorId)) return Forbid();

        await _commentService.DeleteCommentsbyPostIdAsync(id);
        await _postService.DeleteAsync(id);
        return CustomResponse(HttpStatusCode.NoContent);
    }

    private async Task<PostDTO> GetPostByIdAsync(Guid id)
    {
        return _mapper.Map<PostDTO>(await _postRepository.GetByIdAsync(id));
    }
}