using backend.Dtos;
using backend.Entities;
using backend.Infrasctructure;
using Microsoft.EntityFrameworkCore;

namespace backend.Handlers;

public class TodoHandler : ITodoHandler
{
    private readonly ILogger<TodoHandler>   _logger;
    private readonly TodoDbContext          _db;

    public TodoHandler(ILogger<TodoHandler> logger, TodoDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<Todo> CreateAsync(CreateTodoRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Creating todo with title: {Title}", request.Title);

        var todo = new Todo
        {
            // Id = 1,
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _db.Todos.Add(todo);
        await _db.SaveChangesAsync(ct);
        return todo;
    }

    public async Task<List<Todo>> GetByPriorityAsync(IEnumerable<Priority> priorities, CancellationToken ct)
    {
        _logger.LogInformation("Getting a list todo by priorities: {Priorities}", string.Join(", ", priorities));

        return await _db.Todos.Where(t => priorities.Contains(t.Priority)).ToListAsync(ct);
    }
}