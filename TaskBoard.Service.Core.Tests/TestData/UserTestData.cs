using TaskBoard.Framework.Core.Security.Keycloak;
using TaskBoard.Service.Core.Domain.Dtos;

namespace TaskBoard.Service.Core.Tests.TestData;

public static class UserTestData
{
    #region LoginDtos
    public static UserLoginDto USER_LOGIN_1 => new()
    {
        UserName = "testuser",
        PassWord = "password"
    };

    public static UserLoginDto USER_LOGIN_2 => new()
    {
        UserName = "",
        PassWord = "password2"
    };

    public static UserLoginDto USER_LOGIN_VALID => USER_LOGIN_1;

    public static UserLoginDto USER_LOGIN_INVALID => USER_LOGIN_2;
    #endregion

    #region LogoutDtos
    public static UserLogoutDto USER_LOGOUT_1 => new()
    {
        RefreshToken = "mocked_refresh_token1"
    };

    public static UserLogoutDto USER_LOGOUT_2 => new()
    {
        RefreshToken = ""
    };

    public static UserLogoutDto USER_LOGOUT_VALID => USER_LOGOUT_1;

    public static UserLogoutDto USER_LOGOUT_INVALID => USER_LOGOUT_2;
    #endregion

    #region UserTokenOutputDtos
    public static UserTokenOutputDto USER_TOKEN_OUTPUT_1 => new()
    {
        AccessToken = "mocked_token1",
        RefreshToken = "mocked_refresh_token1"
    };

    public static UserTokenOutputDto USER_TOKEN_OUTPUT_2 => new()
    {
        AccessToken = "mocked_token2",
        RefreshToken = "mocked_refresh_token2"
    };
    #endregion

    #region UserTokenOutputDtos
    public static UserTokenRefreshDto USER_TOKEN_REFRESH_1 => new()
    {
        RefreshToken = "mocked_refresh_token1"
    };

    public static UserTokenRefreshDto USER_TOKEN_REFRESH_2 => new()
    {
        RefreshToken = ""
    };

    public static UserTokenRefreshDto USER_TOKEN_REFRESH_VALID => USER_TOKEN_REFRESH_1;

    public static UserTokenRefreshDto USER_TOKEN_REFRESH_INVALID => USER_TOKEN_REFRESH_2;
    #endregion

    #region KeycloakTokenResponses
    public static KeycloakTokenResponse KEYCLOAK_RESPONSE_TOKEN_1 => new()
    {
        AccessToken = "mocked_access_token",
        ExpiresIn = 2234,
        RefreshToken = "mocked_refresh_token",
        RefreshExpiresIn = 23123123,
        TokenType = "token"
    };
    #endregion
}
