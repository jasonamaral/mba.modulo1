using AutoMapper;
using mba.modulo1.blog.domain.Entities;
using MBA.Modulo1.Blog.API.DTO;
using MBA.Modulo1.Blog.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MBA.Modulo1.Blog.API.Controllers;

[Route("api/posts")]
public class PostController : MainController
{
    private readonly IPostService _postService;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostController(IPostService postService, IPostRepository postRepository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        _postService = postService;
        _postRepository = postRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<PostDTO>> GetAll()
    {
        return _mapper.Map<IEnumerable<PostDTO>>(await _postRepository.GetAllAsync());
    }

    [HttpGet("GetById/{id:guid}")]
    public async Task<ActionResult<PostDTO>> GetByIdAsync(Guid id)
    {
        var post = await GetPostByIdAsync(id);

        if (post == null) return NotFound();

        return Ok(post);
    }

    [HttpPost("Add")]
    public async Task<ActionResult<PostDTO>> AddAsync(PostDTO postDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _postRepository.AddAsync(_mapper.Map<Post>(postDTO));
        return CustomResponse(HttpStatusCode.Created, postDTO);
    }

    [HttpPut("Update/{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, PostDTO postDTO)
    {
        if (id != postDTO.Id)
        {
            NotifyError("Os ids informados não são iguais!");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var post = await GetPostByIdAsync(id);
        if (post == null) return NotFound();

        post.UpdatedAt = DateTime.Now;
        post.Title = postDTO.Title;
        post.Content = postDTO.Content;

        await _postService.UpdateAsync(_mapper.Map<Post>(post));
        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("Delete/{id:guid}")]
    public async Task<ActionResult<PostDTO>> DeleteAsync(Guid id)
    {
        var post = await GetPostByIdAsync(id);
        if (post == null) return NotFound();

        await _postService.DeleteAsync(id);
        return CustomResponse(HttpStatusCode.NoContent);
    }

    private async Task<PostDTO> GetPostByIdAsync(Guid id)
    {
        return _mapper.Map<PostDTO>(await _postRepository.GetByIdAsync(id));
    }
}