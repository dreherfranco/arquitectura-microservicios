using System.Text;
using System.Text.Json;
using ArticleService.DTOs;
using ArticleService.SyncDataServices.Http.Interfaces;

namespace ArticleService.SyncDataServices.Http
{
    public class HttpCategoryDataClient : IHttpCategoryDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public HttpCategoryDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<CategoryDTO> GetCategoryById(int categoryExternalId)
        {
            var response = await _httpClient
                          .GetAsync($"{_configuration["CategoryService"]}/get-by-id/{categoryExternalId}");

            if(response.IsSuccessStatusCode)
            {
                var categoryDTO = await response.Content.ReadFromJsonAsync<CategoryDTO>();
            
                Console.WriteLine("--> Sync GET to CategoryService was OK!");
                return categoryDTO;
            }
            else
            {
                Console.WriteLine("--> Sync GET to CategoryService was NOT OK!");
                return null;
            }
        }

        public async Task SendPostRequestToCategories(CategoryDTO categoryDTO)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(categoryDTO),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient
                            .PostAsync($"{_configuration["CategoryService"]}"/*+URL METODO POST*/, httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to CommandService was NOT OK!");
            }
        }
    }
}