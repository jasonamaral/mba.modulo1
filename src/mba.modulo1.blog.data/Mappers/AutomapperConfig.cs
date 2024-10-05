using AutoMapper;
using mba.modulo1.blog.domain.Entities;
using MBA.Modulo1.Blog.DTO;

namespace MBA.Modulo1.Blog.API.Config;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Post, PostDTO>()
            .ForMember(dest => dest.UserName, source => source.MapFrom(src => src.User.UserName))
            .ReverseMap();

        CreateMap<Post, PostSaveDTO>().ReverseMap();

        CreateMap<Comment, CommentDTO>()
            .ForMember(dest => dest.UserName, source => source.MapFrom(src => src.User.UserName))
            .ReverseMap();

        CreateMap<Comment, CommentSaveDTO>().ReverseMap();
        CreateMap<Comment, CommentUpdateDTO>().ReverseMap();
    }
}