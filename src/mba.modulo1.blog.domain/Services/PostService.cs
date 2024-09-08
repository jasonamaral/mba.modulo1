using mba.modulo1.blog.domain.Entities;
using MBA.Modulo1.Blog.Domain.Entities.Validations;
using MBA.Modulo1.Blog.Domain.Interfaces;

namespace MBA.Modulo1.Blog.Domain.Services;

public class PostService : BaseService, IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository, INotifier notifier) : base(notifier)
    {
        _postRepository = postRepository;
    }

    public async Task AddAsync(Post post)
    {
        if (!Validate(new PostValidation(), post)) return;

        await _postRepository.AddAsync(post);
    }

    public async Task UpdateAsync(Post post)
    {
        if (!Validate(new PostValidation(), post)) return;
        await _postRepository.UpdateAsync(post);
    }

    public async Task DeleteAsync(Guid id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post != null)
        {
            Notify("Post não encontrado!");
            return;
        }

        await _postRepository.DeleteByIdAsync(id);
    }

    public void Dispose() => _postRepository.Dispose();
}