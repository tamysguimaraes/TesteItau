using Microsoft.EntityFrameworkCore;
using Products.Data.Context;
using Products.Data.Interface;
using Products.Domain.Mapping;
using Products.Data.Repository;
using Products.Domain.Interface;
using Products.Domain.Service;
using System.ComponentModel.Design;
using AutoMapper;
using System.Reflection;
using Products.Data.Entities;
using Products.Domain.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
bool.TryParse(builder.Configuration.GetSection("Configuracoes:BancoDeDadosInMemory").Value, out bool _bancoDeDadosInMemory);
if (!_bancoDeDadosInMemory)
{
    builder.Services.AddDbContext<APIContext>(opt =>
        opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConn"))
    );
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var config = new MapperConfiguration(cfg => {
    cfg.CreateMap<ProductEntity, Product>().ReverseMap();
});

IMapper mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{

}