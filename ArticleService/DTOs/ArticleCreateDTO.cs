using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleService.DTOs
{
    public class ArticleCreateDTO
    {
        public string Name { get; set; }
        public float Amount { get; set; }
        public int CategoryExternalId { get; set; }
    }
}