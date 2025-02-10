using Microsoft.AspNetCore.Mvc;
using TaskBoard.Framework.Core.Exceptions.Rest;
using TaskBoard.Service.Core.Domain.Dtos;
using TaskBoard.Service.Core.Domain.Services;

namespace TaskBoard.Service.Core.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserTokenOutputDto>> LoginAsync([FromBody] UserLoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestRestException("Invalid input data");
        }

        UserTokenOutputDto result = await _service.LoginAsync(loginDto);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<UserTokenOutputDto>> RefreshTokenAsync([FromBody] UserTokenRefreshDto tokenRefreshDto)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestRestException("Invalid input data");
        }

        UserTokenOutputDto result = await _service.RefreshTokenAsync(tokenRefreshDto);
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> LogoutAsync([FromBody] UserLogoutDto logoutDto)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestRestException("Invalid input data");
        }

        await _service.LogoutAsync(logoutDto);
        return Ok();
    }
}
