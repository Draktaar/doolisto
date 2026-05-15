using backend.Dtos;
using backend.Entities;

namespace backend.Handlers;

public interface ITodoHandler
{
    Task<TodoResponse> CreateAsync(CreateTodoRequest request, CancellationToken ct);
    Task<TodoResponse?> UpdateAsync(long id, UpdateTodoRequest request, CancellationToken ct);
    Task<List<TodoResponse>> GetByPriorityAsync(IEnumerable<Priority> priorities, CancellationToken ct);
    Task<List<long>> DeleteAsync(IEnumerable<long> ids, CancellationToken ct);
}