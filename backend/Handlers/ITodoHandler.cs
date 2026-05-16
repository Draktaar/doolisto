using backend.Common;
using backend.Dtos;
using backend.Entities;

namespace backend.Handlers;

public interface ITodoHandler
{
    Task<Result<TodoResponse>> CreateAsync(CreateTodoRequest request, CancellationToken ct);
    Task<Result<TodoResponse>> UpdateAsync(long id, UpdateTodoRequest request, CancellationToken ct);
    Task<List<TodoResponse>> GetByPriorityAsync(IEnumerable<Priority> priorities, CancellationToken ct);
    Task<Result<List<long>>> DeleteAsync(IEnumerable<long> ids, CancellationToken ct);
}