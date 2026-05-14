using backend.Dtos;
using backend.Handlers;

namespace backend.Endpoints;

public static class TodoEndpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/todos", async (CreateTodoRequest request, ITodoHandler handler, CancellationToken ct) =>
        {
            var todo = await handler.CreateAsync(request, ct);
            return Results.Created($"/todos/{todo.Id}", todo);
        });
    }
}