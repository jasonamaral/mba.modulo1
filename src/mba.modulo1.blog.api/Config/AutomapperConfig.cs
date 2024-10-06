using AutoMapper;
using mba.modulo1.blog.domain.Entities;
using MBA.Modulo1.Blog.DTO;

namespace MBA.Modulo1.Blog.Data.Mappers;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Post, PostDTO>().ReverseMap();
        CreateMap<Post, PostSaveDTO>().ReverseMap();
        CreateMap<Comment, CommentDTO>().ReverseMap();
        CreateMap<Comment, CommentSaveDTO>().ReverseMap();
    }
}