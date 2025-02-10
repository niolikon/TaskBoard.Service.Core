using TaskBoard.Framework.Core.Repositories;
using TaskBoard.Service.Core.Domain.Entities;
using TaskBoard.Service.Core.Domain.Repositories;

namespace TaskBoard.Service.Core.Infrastructure.Repositories;

public class TodoRepository : BaseSecuredCrudRepositoryServedOwner<Todo, int, User>, ITodoRepository
{
    public TodoRepository(AppDbContext context) : base(context) {}
}
