using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Service.Core.Domain.Dtos;

public class UserLoginDto
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string PassWord { get; set; } = string.Empty;
}
