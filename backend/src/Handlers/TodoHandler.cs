using backend.Dtos;
using backend.Entities;

namespace backend.Handlers;

public class TodoHandler : ITodoHandler
{
    private readonly ILogger<TodoHandler> _logger;

    public TodoHandler(ILogger<TodoHandler> logger)
    {
        _logger = logger;
    }

    public async Task<Todo> CreateAsync(CreateTodoRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Creating todo with title: {Title}", request.Title);

        var todo = new Todo
        {
            Id = 1,
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        return todo;
    }
}