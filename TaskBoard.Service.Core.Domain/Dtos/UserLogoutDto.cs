using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Service.Core.Domain.Dtos;

public class UserLogoutDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
