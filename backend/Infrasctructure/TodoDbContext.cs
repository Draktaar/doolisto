using Microsoft.EntityFrameworkCore;
using backend.Entities;

namespace backend.Infrasctructure;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> opt) : base(opt) {}
    public DbSet<Todo> Todos => Set<Todo>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .Property(t => t.Priority)
            .HasConversion<string>();
    }
}