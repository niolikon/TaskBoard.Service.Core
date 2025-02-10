using TaskBoard.Framework.Core.Services;
using TaskBoard.Service.Core.Domain.Dtos;
using TaskBoard.Service.Core.Domain.Entities;
using TaskBoard.Service.Core.Domain.Mappers;
using TaskBoard.Service.Core.Domain.Repositories;

namespace TaskBoard.Service.Core.Domain.Services;

public class TodoService : BaseSecuredCrudService<Todo, int, TodoInputDto, TodoOutputDto, User>, ITodoService
{
    public TodoService(ITodoRepository repository, ITodoMapper mapper) : base(repository, mapper) { }
}
