using TaskBoard.Service.Core.Domain.Dtos;
using TaskBoard.Service.Core.Domain.Entities;

namespace TaskBoard.Service.Core.Domain.Mappers;

public class TodoMapper : ITodoMapper
{
    public Todo MapToEntity(TodoInputDto dto)
    {
        return new Todo() { 
            Description = dto.Description, 
            Title = dto.Title, 
            DueDate = dto.DueDate, 
            IsCompleted = dto.IsCompleted
        };
    }

    public TodoOutputDto MapToOutputDto(Todo entity)
    {
        TodoOutputDto result = new ()
        {
            Id = entity.Id,
            Description = entity.Description,
            Title = entity.Title,
            DueDate = entity.DueDate,
            IsCompleted = entity.IsCompleted
        };

        return result;
    }
}
