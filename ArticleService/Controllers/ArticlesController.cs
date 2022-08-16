using System.Linq.Expressions;
using ArticleService.AsyncDataServices.Events;
using ArticleService.AsyncDataServices.Interfaces;
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
        private readonly IMessageBusClient messageBusClient;
        
        public ArticlesController(Repository<Article> repository, IMapper mapper, 
        IHttpCategoryDataClient httpCategoryDataClient, IMessageBusClient messageBusClient)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.httpCategoryDataClient = httpCategoryDataClient;
            this.messageBusClient = messageBusClient;
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

        [HttpPost("create")]
        public async Task<ActionResult<ArticleDTO>> CreatePlatform([FromBody]ArticleCreateDTO articleCreateDTO)
        {
            var article = mapper.Map<Article>(articleCreateDTO);
            article = await repository.Add(article);

            var articleDTO = mapper.Map<ArticleDTO>(article);

            //Send Async Message
            try
            {
                var articlePublishedDTO = mapper.Map<ArticlePublishedDTO>(articleDTO);
                articlePublishedDTO.Event = ArticleEventType.ARTICLE_PUBLISHED;
                messageBusClient.PublishNewArticle(articlePublishedDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return Ok(articleDTO);
        }
    }
}