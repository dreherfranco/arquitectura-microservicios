using ArticleService.DTOs;
using ArticleService.Models;
using ArticleService.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Controllers
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly Repository<Article> repository;
        private readonly IMapper mapper;

        public ArticlesController(Repository<Article> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet("get-all")]
        public ActionResult<IEnumerable<ArticleDTO>> GetAll()
        {
            var articlesDTO = this.mapper.Map<List<ArticleDTO>>(repository.Get().ToList());
            return Ok(articlesDTO);
        }
        
    }
}