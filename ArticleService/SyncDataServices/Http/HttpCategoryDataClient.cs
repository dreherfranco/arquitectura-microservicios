using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ArticleService.DTOs;
using ArticleService.SyncDataServices.Http.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
           /* var httpContent = new StringContent(
                JsonSerializer.Serialize(categoryExternalId),
                Encoding.UTF8,
                "application/json");*/

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
    }
}