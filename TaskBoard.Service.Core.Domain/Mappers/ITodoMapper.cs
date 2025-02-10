using TaskBoard.Framework.Core.Mappers;
using TaskBoard.Service.Core.Domain.Dtos;
using TaskBoard.Service.Core.Domain.Entities;

namespace TaskBoard.Service.Core.Domain.Mappers;

public interface ITodoMapper: IMapper<Todo, TodoInputDto, TodoOutputDto>
{
}
