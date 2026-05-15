using backend.Dtos;
using backend.Entities;
using backend.Infrasctructure;
using IdGen;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace backend.Handlers;

public class TodoHandler : ITodoHandler
{
    private readonly ILogger<TodoHandler>   _logger;
    private readonly TodoDbContext          _db;
    private readonly IdGenerator            _idGenerator;

    public TodoHandler(ILogger<TodoHandler> logger, TodoDbContext db, IdGenerator idGenerator)
    {
        _logger = logger;
        _db = db;
        _idGenerator = idGenerator;
    }

    public async Task<TodoResponse> CreateAsync(CreateTodoRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Creating todo: {Title}", request.Title);

        var todo = new Todo
        {
            Id = _idGenerator.CreateId(),
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _db.Todos.Add(todo);
        await _db.SaveChangesAsync(ct);
        return TodoResponse.FromTodo(todo);
    }

    public async Task<List<TodoResponse>> GetByPriorityAsync(IEnumerable<Priority> priorities, CancellationToken ct)
    {
        _logger.LogInformation("Getting a list todo by priorities: {Priorities}", string.Join(", ", priorities));

        return await _db.Todos
            .Where(t => priorities.Contains(t.Priority))
            .Select(t => new TodoResponse(
                t.Id,
                t.Title,
                t.Description,
                t.Priority,
                t.CreatedAt,
                t.UpdatedAt))
            .ToListAsync(ct);
    }

    public async Task<TodoResponse?> UpdateAsync(long id, UpdateTodoRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Updating todo: {Title}", request.Title);

        var todo = await _db.Todos.FindAsync(id, ct);

        if (todo is null)
            return null;

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.Priority = request.Priority;
        todo.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
        return TodoResponse.FromTodo(todo);
    }

    public async Task<List<long>> DeleteAsync(IEnumerable<long> ids, CancellationToken ct)
    {
        _logger.LogInformation("Attempt to delete todo: {Ids}", string.Join(", ", ids));

        var todos = await _db.Todos
            .Where(t => ids.Contains(t.Id))
            .ToListAsync(ct);

        var blocked = todos
            .Where(t => t.Priority == Priority.Critical || t.Priority == Priority.High)
            .Select(t => t.Id)
            .ToList();

        if (blocked.Count > 0)
            return blocked;

        _db.Todos.RemoveRange(todos);
        await _db.SaveChangesAsync(ct);
        return blocked;
    }
}