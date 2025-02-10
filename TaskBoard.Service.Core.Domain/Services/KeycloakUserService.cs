using Microsoft.Extensions.Options;
using TaskBoard.Framework.Core.Security.Keycloak;
using TaskBoard.Service.Core.Domain.Dtos;

namespace TaskBoard.Service.Core.Domain.Services;

public class KeycloakUserService : IUserService
{
    private readonly IKeycloakApiClient _keycloakClient;
    private readonly KeycloakOptions _keycloakConfig;

    public KeycloakUserService(IKeycloakApiClient keycloakClient, IOptions<KeycloakOptions> keycloakOptions)
    {
        _keycloakClient = keycloakClient;
        _keycloakConfig = keycloakOptions.Value.Verified();
    }

    public async Task<UserTokenOutputDto> LoginAsync(UserLoginDto loginDto)
    {
        string tokenUrl = $"{_keycloakConfig.RealmUri}/protocol/openid-connect/token";
        FormUrlEncodedContent encodedContent = KeycloakRequestBuilder.Builder()
            .WithUsername(loginDto.UserName)
            .WithPassword(loginDto.PassWord)
            .WithClientId(_keycloakConfig.ClientId)
            .WithClientSecret(_keycloakConfig.ClientSecret)
            .WithPasswordGrantType()
            .Build();
        KeycloakTokenResponse tokenResponse = await _keycloakClient.PostForObjectAsync<KeycloakTokenResponse>(tokenUrl, encodedContent);

        return new UserTokenOutputDto() { AccessToken = tokenResponse.AccessToken, RefreshToken = tokenResponse.RefreshToken };
    }

    public async Task<UserTokenOutputDto> RefreshTokenAsync(UserTokenRefreshDto tokenRefreshDto)
    {
        string refreshUrl = $"{_keycloakConfig.RealmUri}/protocol/openid-connect/token";
        FormUrlEncodedContent encodedContent = KeycloakRequestBuilder.Builder()
            .WithRefreshToken(tokenRefreshDto.RefreshToken)
            .WithClientId(_keycloakConfig.ClientId)
            .WithClientSecret(_keycloakConfig.ClientSecret)
            .WithRefreshTokenGrantType()
            .Build();
        KeycloakTokenResponse tokenResponse = await _keycloakClient.PostForObjectAsync<KeycloakTokenResponse>(refreshUrl, encodedContent);

        return new UserTokenOutputDto() { AccessToken = tokenResponse.AccessToken, RefreshToken = tokenResponse.RefreshToken };
    }

    public async Task LogoutAsync(UserLogoutDto logoutDto)
    {
        string tokenUrl = $"{_keycloakConfig.RealmUri}/protocol/openid-connect/logout";
        FormUrlEncodedContent encodedContent = KeycloakRequestBuilder.Builder()
            .WithRefreshToken(logoutDto.RefreshToken)
            .WithClientId(_keycloakConfig.ClientId)
            .WithClientSecret(_keycloakConfig.ClientSecret)
            .WithRefreshTokenGrantType()
            .Build();
        
        await _keycloakClient.PostAsync(tokenUrl, encodedContent);
    }
}
