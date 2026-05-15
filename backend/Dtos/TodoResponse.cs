using backend.Entities;
using IdGen;

namespace backend.Dtos;

public record TodoResponse(long Id, string Title, string Description, Priority Priority, DateTime CreatedAt, DateTime UpdatedAt)
{
    public static TodoResponse FromTodo(Todo todo) =>
        new(todo.Id, todo.Title, todo.Description, todo.Priority, todo.CreatedAt,todo.UpdatedAt);
}