using AutoMapper;
using mba.modulo1.blog.domain.Entities;
using MBA.Modulo1.Blog.API.DTO;
using MBA.Modulo1.Blog.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MBA.Modulo1.Blog.API.Controllers;

[Route("api/comments")]
public class CommentController : MainController
{
    private readonly ICommentService _commentService;
    private readonly ICommnetRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentController(ICommentService commentService, ICommnetRepository commentRepository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        _commentService = commentService;
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    [HttpGet("GetById/{id:guid}")]
    public async Task<ActionResult<CommentDTO>> GetByIdAsync(Guid id)
    {
        var comment = await GetCommentByIdAsync(id);

        if (comment == null) return NotFound();

        return Ok(comment);
    }

    [HttpPost]
    public async Task<ActionResult<CommentDTO>> Add(CommentDTO commentDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _commentRepository.AddAsync(_mapper.Map<Comment>(commentDTO));
        return CustomResponse(HttpStatusCode.Created, commentDTO);
    }

    [HttpPut("Update/{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, CommentDTO commentDTO)
    {
        if (id != commentDTO.Id)
        {
            NotifyError("Os ids informados não são iguais!");
            return CustomResponse(HttpStatusCode.NoContent);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var comment = await GetCommentByIdAsync(id);
        if (comment == null) return NotFound();

        comment.UpdatedAt = DateTime.Now;
        comment.Content = commentDTO.Content;

        await _commentService.UpdateAsync(_mapper.Map<Comment>(comment));
        return CustomResponse();
    }

    [HttpDelete("Delete/{id:guid}")]
    public async Task<ActionResult<CommentDTO>> DeleteAsync(Guid id)
    {
        var comment = await GetCommentByIdAsync(id);
        if (comment == null) return NotFound();

        await _commentService.DeleteAsync(id);
        return CustomResponse(HttpStatusCode.NoContent);
    }

    private async Task<CommentDTO> GetCommentByIdAsync(Guid id)
    {
        return _mapper.Map<CommentDTO>(await _commentRepository.GetByIdAsync(id));
    }
}