using TaskBoard.Framework.Core.Services;
using TaskBoard.Service.Core.Domain.Dtos;

namespace TaskBoard.Service.Core.Domain.Services;

public interface ITodoService: ISecuredCrudService<int, TodoInputDto, TodoOutputDto> {}
