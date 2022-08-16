
namespace ArticleService.DTOs
{
    public class ArticlePublishedDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public int CategoryExternalId { get; set; }
        public string Event { get; set; }
    }
}