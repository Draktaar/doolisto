using backend.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapTodoEndpoints();

app.Run();
