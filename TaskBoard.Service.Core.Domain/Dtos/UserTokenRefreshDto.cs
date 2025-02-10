using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Service.Core.Domain.Dtos;

public class UserTokenRefreshDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
