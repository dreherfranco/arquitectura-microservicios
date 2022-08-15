
namespace ArticleService.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public int CategoryExternalId { get; set; }
    }
}