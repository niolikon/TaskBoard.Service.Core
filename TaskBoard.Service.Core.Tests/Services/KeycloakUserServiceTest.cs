using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using TaskBoard.Framework.Core.Exceptions.Rest;
using TaskBoard.Framework.Core.Security.Keycloak;
using TaskBoard.Service.Core.Domain.Dtos;
using TaskBoard.Service.Core.Domain.Services;
using TaskBoard.Service.Core.Tests.TestData;

namespace TaskBoard.Service.Core.Tests.Services;

public class KeycloakUserServiceTests
{
    private readonly Mock<IKeycloakApiClient> _mockKeycloakClient;
    private readonly KeycloakOptions _keycloakOptions;
    private readonly Mock<IOptions<KeycloakOptions>> _mockIOptionsKeycloak;
    private readonly KeycloakUserService _service;

    public KeycloakUserServiceTests()
    {
        _mockKeycloakClient = new Mock<IKeycloakApiClient>();
        _keycloakOptions = new KeycloakOptions
        {
            RealmUri = "https://fake-keycloak.com/auth/realms/test",
            ClientId = "test-client",
            ClientSecret = "test-secret"
        };
        _mockIOptionsKeycloak = new Mock<IOptions<KeycloakOptions>>();
        _mockIOptionsKeycloak.Setup(options => options.Value).Returns(_keycloakOptions);

        _service = new KeycloakUserService(_mockKeycloakClient.Object, _mockIOptionsKeycloak.Object);
    }

    [Fact]
    public async Task GivenValidCredentials_WhenLogin_ShouldReturnUserToken()
    {
        UserLoginDto loginDto = UserTestData.USER_LOGIN_VALID;
        KeycloakTokenResponse fakeResponse = UserTestData.KEYCLOAK_RESPONSE_TOKEN_1;
        string expectedUrl = $"{_keycloakOptions.RealmUri}/protocol/openid-connect/token";
        _mockKeycloakClient
            .Setup(client => client.PostForObjectAsync<KeycloakTokenResponse>(expectedUrl, It.IsAny<FormUrlEncodedContent>()))
            .ReturnsAsync(fakeResponse);

        UserTokenOutputDto result = await _service.LoginAsync(loginDto);

        result.Should().NotBeNull();
        result.AccessToken.Should().Be(fakeResponse.AccessToken);
        result.RefreshToken.Should().Be(fakeResponse.RefreshToken);

        _mockKeycloakClient.Verify(
            client => client.PostForObjectAsync<KeycloakTokenResponse>(expectedUrl, It.IsAny<FormUrlEncodedContent>()),
            Times.Once);
    }



    [Fact]
    public async Task GivenInvalidCredentials_WhenLogin_ShouldThrowException()
    {
        UserLoginDto loginDto = UserTestData.USER_LOGIN_INVALID;
        string expectedUrl = $"{_keycloakOptions.RealmUri}/protocol/openid-connect/token";

        _mockKeycloakClient
            .Setup(client => client.PostForObjectAsync<KeycloakTokenResponse>(expectedUrl, It.IsAny<FormUrlEncodedContent>()))
            .ThrowsAsync(new UnauthorizedRestException("Invalid credentials"));

        Func<Task> userLogin = async () => await _service.LoginAsync(loginDto);

        // Assert
        await userLogin.Should().ThrowAsync<UnauthorizedRestException>()
            .WithMessage("Invalid credentials");

        _mockKeycloakClient.Verify(
            client => client.PostForObjectAsync<KeycloakTokenResponse>(expectedUrl, It.IsAny<FormUrlEncodedContent>()),
            Times.Once);
    }

    [Fact]
    public async Task GivenValidToken_WhenRefreshToken_ShouldReturnNewUserToken()
    {
        UserTokenRefreshDto refreshDto = UserTestData.USER_TOKEN_REFRESH_VALID;
        KeycloakTokenResponse fakeResponse = UserTestData.KEYCLOAK_RESPONSE_TOKEN_1;
        string expectedUrl = $"{_keycloakOptions.RealmUri}/protocol/openid-connect/token";
        _mockKeycloakClient
            .Setup(client => client.PostForObjectAsync<KeycloakTokenResponse>(expectedUrl, It.IsAny<FormUrlEncodedContent>()))
            .ReturnsAsync(fakeResponse);

        var result = await _service.RefreshTokenAsync(refreshDto);

        result.Should().NotBeNull();
        result.AccessToken.Should().Be(fakeResponse.AccessToken);
        result.RefreshToken.Should().Be(fakeResponse.RefreshToken);

        _mockKeycloakClient.Verify(
            client => client.PostForObjectAsync<KeycloakTokenResponse>(expectedUrl, It.IsAny<FormUrlEncodedContent>()),
            Times.Once);
    }

    [Fact]
    public async Task GivenInvalidRefreshToken_WhenRefreshToken_ShouldThrowException()
    {
        UserTokenRefreshDto refreshDto = UserTestData.USER_TOKEN_REFRESH_INVALID;
        string expectedUrl = $"{_keycloakOptions.RealmUri}/protocol/openid-connect/token";
        _mockKeycloakClient
            .Setup(client => client.PostForObjectAsync<KeycloakTokenResponse>(expectedUrl, It.IsAny<FormUrlEncodedContent>()))
            .ThrowsAsync(new UnauthorizedRestException("Invalid refresh token"));

        Func<Task> refreshAction = async () => await _service.RefreshTokenAsync(refreshDto);

        await refreshAction.Should().ThrowAsync<UnauthorizedRestException>()
            .WithMessage("Invalid refresh token");

        _mockKeycloakClient.Verify(
            client => client.PostForObjectAsync<KeycloakTokenResponse>(expectedUrl, It.IsAny<FormUrlEncodedContent>()),
            Times.Once);
    }

    [Fact]
    public async Task GivenValidRequest_WhenLogout_ShouldCallKeycloakClient()
    {
        UserLogoutDto logoutDto = UserTestData.USER_LOGOUT_1;
        string expectedUrl = $"{_keycloakOptions.RealmUri}/protocol/openid-connect/logout";

        await _service.LogoutAsync(logoutDto);

        _mockKeycloakClient.Verify(
            client => client.PostAsync(expectedUrl, It.IsAny<FormUrlEncodedContent>()),
            Times.Once);
    }

    [Fact]
    public async Task GivenInvalidRequest_WhenLogout_ShouldThrowException()
    {
        UserLogoutDto logoutDto = UserTestData.USER_LOGOUT_INVALID;
        string expectedUrl = $"{_keycloakOptions.RealmUri}/protocol/openid-connect/logout";
        _mockKeycloakClient
            .Setup(client => client.PostAsync(expectedUrl, It.IsAny<FormUrlEncodedContent>()))
            .ThrowsAsync(new UnauthorizedRestException("Logout failed"));

        Func<Task> logoutAction = async () => await _service.LogoutAsync(logoutDto);

        await logoutAction.Should().ThrowAsync<UnauthorizedRestException>()
            .WithMessage("Logout failed");
        _mockKeycloakClient.Verify(
            client => client.PostAsync(expectedUrl, It.IsAny<FormUrlEncodedContent>()),
            Times.Once);
    }   
}
