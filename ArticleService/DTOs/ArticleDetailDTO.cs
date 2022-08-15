using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleService.DTOs
{
    public class ArticleDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public CategoryDTO CategoryDTO { get; set; } = new CategoryDTO();
    }
}