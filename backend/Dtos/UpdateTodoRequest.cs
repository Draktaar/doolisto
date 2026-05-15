using backend.Entities;

namespace backend.Dtos;

public record UpdateTodoRequest(string Title, string Description, Priority Priority);