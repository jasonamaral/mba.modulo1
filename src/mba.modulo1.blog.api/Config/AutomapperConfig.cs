using AutoMapper;
using mba.modulo1.blog.domain.Entities;
using MBA.Modulo1.Blog.API.DTO;

namespace MBA.Modulo1.Blog.API.Config;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Post, PostDTO>().ReverseMap();
        CreateMap<Comment, CommentDTO>().ReverseMap();
    }
}