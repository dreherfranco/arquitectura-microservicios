using System.Linq.Expressions;
using ArticleService.DTOs;
using ArticleService.Models;
using ArticleService.Repository;
using ArticleService.SyncDataServices.Http.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Controllers
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly Repository<Article> repository;
        private readonly IMapper mapper;
        private readonly IHttpCategoryDataClient httpCategoryDataClient;
        public ArticlesController(Repository<Article> repository, IMapper mapper, IHttpCategoryDataClient httpCategoryDataClient)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.httpCategoryDataClient = httpCategoryDataClient;
        }


        [HttpGet("get-all")]
        public ActionResult<IEnumerable<ArticleDTO>> GetAll()
        {
            var articlesDTO = this.mapper.Map<List<ArticleDTO>>(repository.Get().ToList());
            return Ok(articlesDTO);
        }
        
        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<ArticleDetailDTO>> GetById(int id)
        {
            Expression<Func<Article, bool>> filter = x => x.Id == id;     
            var articleDTO = this.mapper.Map<ArticleDetailDTO>(repository.Get(filter).FirstOrDefault());

            try
            {
                //Prueba de metodo Get con HttpClient
                var categoryDTO = await this.httpCategoryDataClient.GetCategoryById(articleDTO.CategoryDTO.Id);
                articleDTO.CategoryDTO = categoryDTO;
            }catch(Exception ex)
            {
                Console.WriteLine($"--> Could not get synchronously: {ex.Message}");
            }
            return Ok(articleDTO);
        }
    }
}