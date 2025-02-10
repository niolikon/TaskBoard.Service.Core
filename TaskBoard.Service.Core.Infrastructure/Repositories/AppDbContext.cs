using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Service.Core.Domain.Entities;

namespace TaskBoard.Service.Core.Infrastructure.Repositories;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Todo>()
            .ToTable("Todos");
    }
}
