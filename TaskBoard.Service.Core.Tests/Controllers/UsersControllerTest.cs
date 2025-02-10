using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Framework.Core.Exceptions.Rest;
using TaskBoard.Service.Core.Api.Controllers;
using TaskBoard.Service.Core.Domain.Services;
using TaskBoard.Service.Core.Domain.Dtos;
using TaskBoard.Service.Core.Tests.TestData;

namespace TaskBoard.Service.Core.Tests.Controllers;

public class UsersControllerTests
{
    private readonly UsersController _controller;
    private readonly Mock<IUserService> _mockService;

    public UsersControllerTests()
    {
        _mockService = new Mock<IUserService>();
        _controller = new UsersController(_mockService.Object);
    }

    [Fact]
    public async Task GivenValidCredentials_WhenLogin_ShouldReturnUserToken()
    {
        UserLoginDto loginDto = UserTestData.USER_LOGIN_VALID;
        UserTokenOutputDto expectedToken = UserTestData.USER_TOKEN_OUTPUT_1;
        _mockService.Setup(s => s.LoginAsync(loginDto)).ReturnsAsync(expectedToken);

        ActionResult<UserTokenOutputDto> result = await _controller.LoginAsync(loginDto);

        OkObjectResult okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        UserTokenOutputDto actualToken = okResult.Value.Should().BeAssignableTo<UserTokenOutputDto>().Subject;
        actualToken.Should().BeEquivalentTo(expectedToken);
    }

    [Fact]
    public async Task GivenInvalidModelState_WhenLogin_ShouldThrowBadRequestException()
    {
        UserLoginDto loginDto = UserTestData.USER_LOGIN_INVALID;
        _controller.ModelState.AddModelError("Username", "Required");

        Func<Task> userLogin = async () => await _controller.LoginAsync(loginDto);

        await userLogin.Should().ThrowAsync<BadRequestRestException>()
            .WithMessage("Invalid input data");
    }

    [Fact]
    public async Task GivenValidToken_WhenRefreshToken_ShouldReturnNewUserToken()
    {
        UserTokenRefreshDto tokenRefreshDto = UserTestData.USER_TOKEN_REFRESH_VALID;
        UserTokenOutputDto expectedToken = UserTestData.USER_TOKEN_OUTPUT_1;
        _mockService.Setup(s => s.RefreshTokenAsync(tokenRefreshDto)).ReturnsAsync(expectedToken);

        ActionResult<UserTokenOutputDto> result = await _controller.RefreshTokenAsync(tokenRefreshDto);

        OkObjectResult okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        UserTokenOutputDto actualToken = okResult.Value.Should().BeAssignableTo<UserTokenOutputDto>().Subject;
        actualToken.Should().BeEquivalentTo(expectedToken);
    }

    [Fact]
    public async Task GivenInvalidModelState_WhenRefreshToken_ShouldThrowBadRequestException()
    {

        UserTokenRefreshDto tokenRefreshDto = UserTestData.USER_TOKEN_REFRESH_INVALID;
        _controller.ModelState.AddModelError("RefreshToken", "Required");

        Func<Task> tokenRefresh = async () => await _controller.RefreshTokenAsync(tokenRefreshDto);

        await tokenRefresh.Should().ThrowAsync<BadRequestRestException>()
            .WithMessage("Invalid input data");
    }

    [Fact]
    public async Task GivenValidRequest_WhenLogout_ShouldReturnOk()
    {
        UserLogoutDto logoutDto = UserTestData.USER_LOGOUT_VALID;
        _mockService.Setup(s => s.LogoutAsync(logoutDto)).Returns(Task.CompletedTask);

        ActionResult result = await _controller.LogoutAsync(logoutDto);

        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task GivenInvalidModelState_WhenLogout_ShouldThrowBadRequestException()
    {
        UserLogoutDto logoutDto = UserTestData.USER_LOGOUT_INVALID;
        _controller.ModelState.AddModelError("RefreshToken", "Required");

        Func<Task> userLogout = async () => await _controller.LogoutAsync(logoutDto);

        await userLogout.Should().ThrowAsync<BadRequestRestException>()
            .WithMessage("Invalid input data");
    }
}
