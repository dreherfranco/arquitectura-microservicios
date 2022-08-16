using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleService.DTOs;

namespace ArticleService.AsyncDataServices.Interfaces
{
    public interface IMessageBusClient
    {
        void PublishNewArticle(ArticlePublishedDTO articlePublishedDto);
    }
}