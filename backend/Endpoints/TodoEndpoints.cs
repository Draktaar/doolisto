using backend.Entities;
using backend.Dtos;
using backend.Handlers;
using Microsoft.AspNetCore.Mvc;

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

        app.MapGet("/todos", async ([FromQuery] Priority[] priority, ITodoHandler handler, CancellationToken ct) =>
        {
            var todos = await handler.GetByPriorityAsync(priority, ct);
            return Results.Ok(todos);
        });

        app.MapPut("/todos/{id:long}", async (long id, UpdateTodoRequest request, ITodoHandler handler, CancellationToken ct) =>
        {
            var todo = await handler.UpdateAsync(id, request, ct);
            return todo is null ? Results.NotFound() : Results.Ok(todo);
        });

        app.MapDelete("/todos", async ([FromQuery] long[] id, ITodoHandler handler, CancellationToken ct ) =>
        {
            var blocked = await handler.DeleteAsync(id, ct);
            return blocked.Count == 0 ? Results.NoContent() : Results.BadRequest(new { blockedIds = blocked });
        });
    }
}