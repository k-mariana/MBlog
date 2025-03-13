using AutoMapper;
using MBlog.API.Models.Domain;
using MBlog.API.Models.DTO;

namespace MBlog.API.Mappings;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<AddUserRequestDto, User>().ReverseMap();
        CreateMap<UpdateUserRequestDto, User>().ReverseMap();
        CreateMap<AddArticleRequestDto, Article>().ReverseMap();
        CreateMap<Article, ArticleDto>().ReverseMap();
        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<UpdateArticleRequestDto, Article>().ReverseMap();
    }
}
