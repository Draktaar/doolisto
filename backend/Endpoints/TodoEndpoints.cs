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
            if (ValidationHelper.Validate(request) is { } errorResult)
                return errorResult;

            var result = await handler.CreateAsync(request, ct);
            return result.IsSuccess
                ? Results.Created($"/todos/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        });

        app.MapPut("/todos/{id:long}", async (long id, UpdateTodoRequest request, ITodoHandler handler, CancellationToken ct) =>
        {
            if (ValidationHelper.Validate(request) is { } errorResult)
                return errorResult;

            var result = await handler.UpdateAsync(id, request, ct);

            if (result!.IsSuccess)
                return Results.Ok(result.Value);

            return result.Error.Code switch
            {
                "todo.not_found" => Results.NotFound(new { error = result.Error }),
                _ => Results.BadRequest(new { error = result.Error })
            };
        });

        app.MapGet("/todos", async ([FromQuery] Priority[] priority, ITodoHandler handler, CancellationToken ct) =>
        {
            var todos = await handler.GetByPriorityAsync(priority, ct);
            return Results.Ok(todos);
        });

        app.MapDelete("/todos", async ([FromQuery] long[] id, ITodoHandler handler, CancellationToken ct ) =>
        {
            var result = await handler.DeleteAsync(id, ct);
            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(new { error = result.Error });
        });
    }
}