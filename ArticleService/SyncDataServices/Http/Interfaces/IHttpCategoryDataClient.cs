using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleService.DTOs;

namespace ArticleService.SyncDataServices.Http.Interfaces
{
    public interface IHttpCategoryDataClient
    {
        Task<CategoryDTO> GetCategoryById(int categoryExternalId); 
    }
}