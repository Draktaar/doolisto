using backend.Entities;

using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record CreateTodoRequest(
    [Required, MinLength(1), MaxLength(200)] string Title,
    [MaxLength(2000)] string Description,
    Priority Priority);