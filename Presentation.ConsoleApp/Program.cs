

using Infrastructure.Interfaces;
using Infrastructure.Managers;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ConsoleApp;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddSingleton<IJsonFileRepository, JsonFileRepository>();
builder.Services.AddSingleton<IProductManager, ProductManager>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<MenuDialogs>();



var app = builder.Build();

var menuDialogs = app.Services.GetRequiredService<MenuDialogs>();

await menuDialogs.Run();

