using TaskBoard.Framework.Core.Repositories;
using TaskBoard.Service.Core.Domain.Entities;

namespace TaskBoard.Service.Core.Domain.Repositories;

public interface ITodoRepository : ISecuredCrudRepository<Todo, int, User>
{
}
