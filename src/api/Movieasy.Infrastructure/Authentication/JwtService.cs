using Microsoft.Extensions.Options;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Users.LoginUser;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Users;
using Movieasy.Infrastructure.Authentication.Models;
using System.Net.Http.Json;

namespace Movieasy.Infrastructure.Authentication
{
    internal sealed class JwtService : IJwtService
    {
        private static readonly Error AuthenticationFailed = new(
            "Keycloak.AuthenticationFailed",
            "Failed to acquire access token due to an internal authentication failure");

        private readonly HttpClient _httpClient;
        private readonly KeycloakOptions _keycloakOptions;

        public JwtService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions)
        {
            _httpClient = httpClient;
            _keycloakOptions = keycloakOptions.Value;
        }

        public async Task<Result<JwtServiceResult>> GetAccessTokenAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var authRequestParameters = new KeyValuePair<string, string>[]
                {
                    new("client_id", _keycloakOptions.AuthClientId),
                    new("client_secret", _keycloakOptions.AuthClientSecret),
                    new("scope", "openid email"),
                    new("grant_type", "password"),
                    new("username", email),
                    new("password", password)
                };

                var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

                var response = await _httpClient.PostAsync("", authorizationRequestContent, cancellationToken);

                response.EnsureSuccessStatusCode();

                var authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

                if (authorizationToken == null)
                {
                    return Result.Failure<JwtServiceResult>(AuthenticationFailed);
                }

                return new JwtServiceResult(
                    authorizationToken.AccessToken,
                    authorizationToken.RefreshToken);
            }
            catch (HttpRequestException)
            {
                return Result.Failure<JwtServiceResult>(AuthenticationFailed);
            }
        }

        public async Task<Result<JwtServiceResult>> RefreshTokenAsync(
            string refreshToken,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var authRequestParameters = new KeyValuePair<string, string>[]
                {
                    new("client_id", _keycloakOptions.AuthClientId),
                    new("client_secret", _keycloakOptions.AuthClientSecret),
                    new("scope", "openid email"),
                    new("grant_type", "refresh_token"),
                    new("refresh_token", refreshToken),
                };

                var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

                var response = await _httpClient.PostAsync("", authorizationRequestContent, cancellationToken);

                response.EnsureSuccessStatusCode();

                var authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

                if (authorizationToken == null)
                {
                    return Result.Failure<JwtServiceResult>(AuthenticationFailed);
                }

                return new JwtServiceResult(
                    authorizationToken.AccessToken,
                    authorizationToken.RefreshToken);
            }
            catch (HttpRequestException)
            {
                return Result.Failure<JwtServiceResult>(AuthenticationFailed);
            }
        }
    }
}
