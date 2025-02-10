using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskBoard.Framework.Core.Security.Authentication;
using TaskBoard.Service.Core.Api.Controllers;
using TaskBoard.Service.Core.Domain.Dtos;
using TaskBoard.Service.Core.Domain.Services;
using TaskBoard.Service.Core.Tests.TestData;

namespace TaskBoard.Service.Core.Tests.Controllers;

public class TodosControllerTest
{
    private readonly Mock<ITodoService> _todosServiceMock;
    private readonly Mock<IAuthenticatedUserService> _authenticatedUserServiceMock;
    private readonly TodosController _controller;

    public TodosControllerTest()
    {
        _todosServiceMock = new Mock<ITodoService>();
        _authenticatedUserServiceMock = new Mock<IAuthenticatedUserService>();
        _controller = new TodosController(_todosServiceMock.Object, _authenticatedUserServiceMock.Object);
    }

    [Fact]
    public async Task GivenValidInput_WhenCreate_ThenRequestIsRelayed()
    {
        TodoInputDto inputDto = TodoTestData.TODO_1_INPUT;
        AuthenticatedUser authenticatedUserDummy = (new Mock<AuthenticatedUser>()).Object;
        _authenticatedUserServiceMock
            .Setup(authenticatedUserService => authenticatedUserService.User)
            .Returns(authenticatedUserDummy);
        TodoOutputDto outputDto = TodoTestData.TODO_1_OUTPUT;
        _todosServiceMock
            .Setup(service => service.CreateAsync(inputDto, It.IsAny<AuthenticatedUser>()))
            .ReturnsAsync(outputDto);

        ActionResult<TodoOutputDto> result = await _controller.Create(inputDto);

        CreatedAtActionResult createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
        TodoOutputDto actualResultDto = createdResult.Value.Should().BeOfType<TodoOutputDto>().Subject;
        actualResultDto.Should().BeEquivalentTo(outputDto);
        _todosServiceMock.Verify(service => service.CreateAsync(inputDto, authenticatedUserDummy), Times.Once());
    }

    [Fact]
    public async Task GivenValidInput_WhenRead_ThenRequestIsRelayed()
    {
        AuthenticatedUser authenticatedUserDummy = (new Mock<AuthenticatedUser>()).Object;
        _authenticatedUserServiceMock
            .Setup(authenticatedUserService => authenticatedUserService.User)
            .Returns(authenticatedUserDummy);
        TodoOutputDto outputDto = TodoTestData.TODO_2_OUTPUT;
        int todoId = outputDto.Id;
        _todosServiceMock
            .Setup(service => service.ReadAsync(todoId, It.IsAny<AuthenticatedUser>()))
            .ReturnsAsync(outputDto);

        ActionResult<TodoOutputDto> result = await _controller.ReadAsync(todoId);

        OkObjectResult okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        TodoOutputDto actualResultDto = okResult.Value.Should().BeOfType<TodoOutputDto>().Subject;
        actualResultDto.Should().BeEquivalentTo(outputDto);
        _todosServiceMock.Verify(service => service.ReadAsync(todoId, authenticatedUserDummy), Times.Once());
    }

    [Fact]
    public async Task GivenValidInput_WhenReadAll_ThenRequestIsRelayed()
    {
        AuthenticatedUser authenticatedUserDummy = (new Mock<AuthenticatedUser>()).Object;
        _authenticatedUserServiceMock
            .Setup(authenticatedUserService => authenticatedUserService.User)
            .Returns(authenticatedUserDummy);
        IEnumerable<TodoOutputDto> outputDtos = [TodoTestData.TODO_2_OUTPUT, TodoTestData.TODO_1_OUTPUT];
        _todosServiceMock
            .Setup(service => service.ReadAllAsync(It.IsAny<AuthenticatedUser>()))
            .ReturnsAsync(outputDtos);

        ActionResult<IEnumerable<TodoOutputDto>> result = await _controller.ReadAllAsync();

        OkObjectResult okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        IEnumerable<TodoOutputDto> actualResultDto = okResult.Value.Should()
            .BeAssignableTo<IEnumerable<TodoOutputDto>>()
            .Subject.As<IEnumerable<TodoOutputDto>>();
        actualResultDto.Should().BeEquivalentTo(outputDtos);
        _todosServiceMock.Verify(service => service.ReadAllAsync(authenticatedUserDummy), Times.Once());
    }

    [Fact]
    public async Task GivenValidInput_WhenUpdate_ThenRequestIsRelayed()
    {
        AuthenticatedUser authenticatedUserDummy = (new Mock<AuthenticatedUser>()).Object;
        _authenticatedUserServiceMock
            .Setup(authenticatedUserService => authenticatedUserService.User)
            .Returns(authenticatedUserDummy);
        TodoInputDto inputDto = TodoTestData.TODO_1_INPUT;
        TodoOutputDto outputDto = TodoTestData.TODO_1_OUTPUT;
        int todoId = outputDto.Id;
        _todosServiceMock
            .Setup(service => service.UpdateAsync(todoId, inputDto, It.IsAny<AuthenticatedUser>()))
            .ReturnsAsync(outputDto);

        ActionResult<TodoOutputDto> result = await _controller.UpdateAsync(todoId, inputDto);

        OkObjectResult okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        TodoOutputDto actualResultDto = okResult.Value.Should().BeOfType<TodoOutputDto>().Subject;
        actualResultDto.Should().BeEquivalentTo(outputDto);
        _todosServiceMock.Verify(service => service.UpdateAsync(todoId, inputDto, authenticatedUserDummy), Times.Once());
    }

    [Fact]
    public async Task GivenValidInput_WhenDelete_ThenRequestIsRelayed()
    {
        AuthenticatedUser authenticatedUserDummy = (new Mock<AuthenticatedUser>()).Object;
        _authenticatedUserServiceMock
            .Setup(authenticatedUserService => authenticatedUserService.User)
            .Returns(authenticatedUserDummy);
        int todoId = 2;

        ActionResult<TodoOutputDto> result = await _controller.DeleteAsync(todoId);

        result.Result.Should().BeOfType<NoContentResult>();
        _todosServiceMock.Verify(service => service.DeleteAsync(todoId, authenticatedUserDummy), Times.Once());
    }
}
