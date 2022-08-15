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
        }
    }
}