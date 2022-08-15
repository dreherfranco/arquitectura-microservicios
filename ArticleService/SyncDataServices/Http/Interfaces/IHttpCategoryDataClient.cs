using ArticleService.DTOs;

namespace ArticleService.SyncDataServices.Http.Interfaces
{
    public interface IHttpCategoryDataClient
    {
        Task<CategoryDTO> GetCategoryById(int categoryExternalId); 
        Task SendPostRequestToCategories(CategoryDTO categoryDTO);
    }
}