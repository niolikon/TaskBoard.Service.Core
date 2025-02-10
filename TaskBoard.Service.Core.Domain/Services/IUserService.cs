using TaskBoard.Service.Core.Domain.Dtos;

namespace TaskBoard.Service.Core.Domain.Services;

public interface IUserService
{
    Task<UserTokenOutputDto> LoginAsync(UserLoginDto loginDto);

    Task<UserTokenOutputDto> RefreshTokenAsync(UserTokenRefreshDto tokenRefreshDto);

    Task LogoutAsync(UserLogoutDto logoutDto);
}
