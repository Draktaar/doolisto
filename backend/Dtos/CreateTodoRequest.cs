using backend.Entities;

namespace backend.Dtos;

public record CreateTodoRequest(string Title, string Description, Priority Priority);