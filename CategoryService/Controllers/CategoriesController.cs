using AutoMapper;
using CategoryService.DTOs.CategoryDTO;
using CategoryService.Models;
using CategoryService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CategoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly Repository<Category> repository;
        private readonly IMapper mapper;

        public CategoriesController(Repository<Category> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("get-all")]
        public ActionResult<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = this.repository.Get().ToList();
            
            return Ok(this.mapper.Map<List<CategoryDTO>>(categories));
        }
        
        
    }
}