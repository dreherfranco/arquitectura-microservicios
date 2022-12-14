using ArticleService.DTOs;
using ArticleService.Models;
using AutoMapper;

namespace CategoryService.Mapper
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            CreateMap<Article, ArticleDTO>()
                .ReverseMap();

            CreateMap<ArticleCreateDTO, Article>();

            CreateMap<Article, ArticleDetailDTO>()
                .ForPath( dest => dest.CategoryDTO.Id, 
                    opt => opt.MapFrom(
                        src=>src.CategoryExternalId)
                    )
                .ForPath( dest => dest.CategoryDTO.Name, opt => opt.Ignore());

            CreateMap<ArticleDTO, ArticlePublishedDTO>()
                .ForMember(dest => dest.Event, opt => opt.Ignore());
        }
    }
}