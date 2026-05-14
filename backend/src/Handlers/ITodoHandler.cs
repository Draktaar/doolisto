using backend.Dtos;
using backend.Entities;

namespace backend.Handlers;

public interface ITodoHandler
{
    Task<Todo> CreateAsync(CreateTodoRequest request, CancellationToken ct);
    // Task<Todo?> UpdateAsync(long id, UpdateTodoRequest request, CancellationToken ct);
    // Task<List<Todo>> GetByPriorityAsync(IEnumerable<Priority> priorities, CancellationToken ct);
    // Task<bool> DeleteAsync(IEnumerable<long> ids, CancellationToken ct);
}