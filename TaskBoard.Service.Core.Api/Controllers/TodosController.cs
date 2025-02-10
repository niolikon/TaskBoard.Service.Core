using TaskBoard.Framework.Core.Controllers;
using TaskBoard.Framework.Core.Security.Authentication;
using TaskBoard.Service.Core.Domain.Dtos;
using TaskBoard.Service.Core.Domain.Services;

namespace TaskBoard.Service.Core.Api.Controllers;

public class TodosController(ITodoService service, IAuthenticatedUserService userService) : BaseSecuredCrudController<int, TodoInputDto, TodoOutputDto>(service, userService) {}
