using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Service.Core.Domain.Dtos;

public class TodoInputDto
{
    [Required]
    [MaxLength(30)]
    public required string Title { get; set; }

    [Required]
    [MaxLength(250)]
    public required string Description { get; set; }

    public bool IsCompleted { get; set; } = false;

    public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddDays(15));
}
