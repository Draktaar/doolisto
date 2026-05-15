using backend.Handlers;
using backend.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITodoHandler, TodoHandler>();

var app = builder.Build();

app.MapTodoEndpoints();

app.Run();
