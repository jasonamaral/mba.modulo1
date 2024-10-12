﻿using AutoMapper;
using mba.modulo1.blog.domain.Entities;
using MBA.Modulo1.Blog.Domain.DTO;
using MBA.Modulo1.Blog.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MBA.Modulo1.Blog.API.Controllers;

[Authorize]
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

    [AllowAnonymous]
    [HttpGet("GetByPostId/{id:guid}")]
    public async Task<IEnumerable<CommentDTO>> GetByIdAsync(Guid id)
    {
        var comments = _mapper.Map<IEnumerable<CommentDTO>>(await _commentRepository.GetCommentsByPostAsync(id));

        return comments;
    }

    [HttpPost]
    public async Task<ActionResult<CommentSaveDTO>> Add(CommentSaveDTO commentDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var comment = _mapper.Map<Comment>(commentDTO);
        comment.Id = Guid.NewGuid();  // Ensure a new ID is generated
        comment.AuthorId = GetLoggedUser();

        await _commentRepository.AddAsync(comment);
        return CustomResponse(HttpStatusCode.Created, commentDTO);
    }

    [HttpPut("Update/{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, CommentUpdateDTO commentDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) return NotFound();

        if (!UserHasPermition(new Guid(comment.AuthorId))) return Forbid();

        comment.Content = commentDTO.Content;

        await _commentService.UpdateAsync(_mapper.Map<Comment>(comment));

        return CustomResponse();
    }

    [HttpDelete("Delete/{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var comment = await GetCommentByIdAsync(id);
        if (comment == null) return NotFound();
        if (!UserHasPermition(comment.AuthorId)) return Forbid();

        await _commentService.DeleteAsync(id);
        return CustomResponse(HttpStatusCode.NoContent);
    }

    private async Task<CommentDTO> GetCommentByIdAsync(Guid id)
    {
        return _mapper.Map<CommentDTO>(await _commentRepository.GetByIdAsync(id));
    }
}