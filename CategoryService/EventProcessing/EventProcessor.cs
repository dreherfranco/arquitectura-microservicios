using System.Text.Json;
using AutoMapper;
using CategoryService.AsyncDataServices.Events;
using CategoryService.DTOs.ArticleDTO;
using CategoryService.DTOs.Events;
using CategoryService.EventProcessing.Interfaces;

namespace CategoryService.EventProcessing
{
    public class EventProcessor: IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case ArticleEventType.ArticlePublished:
                    addArticle(message);
                    break;
                default:
                    break;
            }
        }

        private ArticleEventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDTO>(notifcationMessage);

            switch(eventType.Event)
            {
                case "Article_Published":
                    Console.WriteLine("--> Article Published Event Detected");
                    return ArticleEventType.ArticlePublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return ArticleEventType.Undetermined;
            }
        }

        private void addArticle(string articlePublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
               // var repo = scope.ServiceProvider.GetRequiredService<Repository<Article>>();
                
                var articlePublishedDto = JsonSerializer.Deserialize<ArticlePublishedDTO>(articlePublishedMessage);

                try
                {
                    //persistir la data en la bdd si se lo requiere...
                    //var article = mapper.Map<Article>(articlePublishedDto) ....
                    //repo.Add.(article)
                    Console.WriteLine("AGREGANDO ARTICULO SATISFACTORIAMENTE.........");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not to proccess the message: {ex.Message}");
                }
            }
        }
    }
}