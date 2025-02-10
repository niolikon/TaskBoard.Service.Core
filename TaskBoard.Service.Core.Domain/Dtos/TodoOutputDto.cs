using TaskBoard.Framework.Core.Dtos;

namespace TaskBoard.Service.Core.Domain.Dtos;

public class TodoOutputDto : BaseOutputDto<int>
{
    public string Title { get; set; }

    public string Description { get; set; }

    public bool? IsCompleted { get; set; }

    public DateOnly? DueDate { get; set; }
}
