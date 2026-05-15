using backend.Handlers;
using backend.Endpoints;
using backend.Infrasctructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITodoHandler, TodoHandler>();
builder.Services.AddDbContext<TodoDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapTodoEndpoints();

app.Run();
