using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Service.Core.Domain.Dtos;

public class UserTokenOutputDto
{
    [Required]
    public string AccessToken { get; set; } = string.Empty;

    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
