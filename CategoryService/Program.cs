using CategoryService.AsyncDataServices;
using CategoryService.DbConfiguration;
using CategoryService.EventProcessing;
using CategoryService.EventProcessing.Interfaces;
using CategoryService.Models;
using CategoryService.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("CategoriesConnection")));


builder.Services.AddTransient<Repository<Category>>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Subscriber RabbitMQ
builder.Services.AddHostedService<MessageBusSubscriber>();
//Procesador de eventos recibidos
builder.Services.AddSingleton<IEventProcessor, EventProcessor>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

DbSeedData.PrepPopulation(app, app.Environment.IsDevelopment());

app.Run();
