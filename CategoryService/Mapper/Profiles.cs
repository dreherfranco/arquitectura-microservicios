using AutoMapper;
using CategoryService.DTOs.CategoryDTO;
using CategoryService.Models;

namespace CategoryService.Mapper
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<Category, CategoryDTO>()
                .ReverseMap();
        }
    }
}