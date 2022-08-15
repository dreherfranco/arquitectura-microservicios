using ArticleService.Models;
using ArticleService.Repository;
using ArticleService.SyncDataServices.Http;
using ArticleService.SyncDataServices.Http.Interfaces;
using CategoryService.DbConfiguration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlite(builder.Configuration.GetConnectionString("ArticlesConnection")));


builder.Services.AddTransient<Repository<Article>>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

builder.Services.AddHttpClient<IHttpCategoryDataClient, HttpCategoryDataClient>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
