using FluentAssertions;
using Moq;
using TaskBoard.Framework.Core.Exceptions.Persistence;
using TaskBoard.Framework.Core.Exceptions.Rest;
using TaskBoard.Framework.Core.Security.Authentication;
using TaskBoard.Service.Core.Domain.Dtos;
using TaskBoard.Service.Core.Domain.Entities;
using TaskBoard.Service.Core.Domain.Mappers;
using TaskBoard.Service.Core.Domain.Repositories;
using TaskBoard.Service.Core.Domain.Services;
using TaskBoard.Service.Core.Tests.TestData;

namespace TaskBoard.Service.Core.Tests.Services;

public class TodoServiceTests
{
    private readonly Mock<ITodoRepository> _repositoryMock;
    private readonly Mock<ITodoMapper> _mapperMock;
    private readonly TodoService _service;
    private readonly AuthenticatedUser _authenticatedUser;
    private readonly User _user;

    public TodoServiceTests()
    {
        _repositoryMock = new Mock<ITodoRepository>();
        _mapperMock = new Mock<ITodoMapper>();
        _service = new TodoService(_repositoryMock.Object, _mapperMock.Object);
        _authenticatedUser = new AuthenticatedUser { Id = "user123" };
        _user = new User { Id = "user123" };
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedTodo()
    {
        var inputDto = TodoTestData.TODO_1_INPUT;
        var entity = TodoTestData.TODO_1;
        _mapperMock.Setup(m => m.MapToEntity(inputDto)).Returns(entity);
        _repositoryMock.Setup(r => r.CreateAsync(entity, _user)).ReturnsAsync(entity);
        var outputDto = TodoTestData.TODO_2_OUTPUT;
        _mapperMock.Setup(m => m.MapToOutputDto(entity)).Returns(outputDto);

        var result = await _service.CreateAsync(inputDto, _authenticatedUser);

        result.Should().NotBeNull();
        result.Id.Should().Be(outputDto.Id);
    }

    [Fact]
    public async Task Create_WhenRepositoryFails_ShouldThrowConflictRestException()
    {
        var inputDto = TodoTestData.TODO_2_INPUT;
        var entity = TodoTestData.TODO_2;
        _mapperMock.Setup(m => m.MapToEntity(inputDto)).Returns(entity);
        _repositoryMock.Setup(r => r.CreateAsync(entity, _user)).ThrowsAsync(new RepositorySaveChangeFailedException("Could not save entity"));

        Func<Task<TodoOutputDto>> todoCreation = async () => await _service.CreateAsync(inputDto, _authenticatedUser);

        await todoCreation.Should().ThrowAsync<ConflictRestException>();
    }

    [Fact]
    public async Task ReadAllAsync_ShouldReturnAllTodos()
    {
        List<Todo> entities = [TodoTestData.TODO_1, TodoTestData.TODO_2];
        List<TodoOutputDto> outputDtos = [TodoTestData.TODO_1_OUTPUT, TodoTestData.TODO_2_OUTPUT];
        _repositoryMock.Setup(r => r.ReadAllAsync(_user)).ReturnsAsync(entities);
        _mapperMock.Setup(m => m.MapToOutputDto(entities[0])).Returns(outputDtos[0]);
        _mapperMock.Setup(m => m.MapToOutputDto(entities[1])).Returns(outputDtos[1]);

        var result = await _service.ReadAllAsync(_authenticatedUser);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(outputDtos);
    }

    [Fact]
    public async Task ReadAsync_WhenEntityNotFound_ShouldThrowNotFoundRestException()
    {
        _repositoryMock.Setup(r => r.ReadAsync(1, _user)).ThrowsAsync(new EntityNotFoundException("Not found"));

        Func<Task<TodoOutputDto>> todoRead = async () => await _service.ReadAsync(1, _authenticatedUser);

        await todoRead.Should().ThrowAsync<NotFoundRestException>();
    }

    [Fact]
    public async Task UpdateAsync_WhenEntityNotFound_ShouldThrowNotFoundRestException()
    {
        TodoInputDto inputDto = TodoTestData.TODO_2_INPUT;
        Todo entity = TodoTestData.TODO_2;
        _mapperMock.Setup(m => m.MapToEntity(inputDto)).Returns(entity);
        _repositoryMock.Setup(r => r.UpdateAsync(entity, _user)).ThrowsAsync(new EntityNotFoundException("Not found"));

        Func<Task<TodoOutputDto>> todoUpdate = async () => await _service.UpdateAsync(1, inputDto, _authenticatedUser);

        await todoUpdate.Should().ThrowAsync<NotFoundRestException>();
    }

    [Fact]
    public async Task DeleteAsync_WhenEntityNotFound_ShouldThrowNotFoundRestException()
    {
        _repositoryMock.Setup(r => r.DeleteAsync(1, _user)).ThrowsAsync(new EntityNotFoundException("Not found"));

        Func<Task> todoDelete = async () => await _service.DeleteAsync(1, _authenticatedUser);

        await todoDelete.Should().ThrowAsync<NotFoundRestException>();
    }
}
