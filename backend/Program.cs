using backend.Handlers;
using backend.Endpoints;
using backend.Infrasctructure;

using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using IdGen;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    opt.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter(
            namingPolicy: JsonNamingPolicy.SnakeCaseLower,
            allowIntegerValues: false)); 
});

builder.Services.AddScoped<ITodoHandler, TodoHandler>();

builder.Services.AddSingleton(new IdGenerator(0));

builder.Services.AddDbContext<TodoDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSnakeCaseNamingConvention());

var app = builder.Build();

app.MapTodoEndpoints();

app.Run();
