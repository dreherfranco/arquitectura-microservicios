using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArticleService.Controllers
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(ILogger<ArticlesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get-all")]
        public ActionResult<IEnumerable<TModel>> GetAll()
        {
            return null;
        }
        
    }
}