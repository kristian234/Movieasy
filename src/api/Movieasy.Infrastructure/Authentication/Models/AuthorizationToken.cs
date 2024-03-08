using System.Text.Json.Serialization;

namespace Movieasy.Infrastructure.Authentication.Models;

internal sealed class AuthorizationToken
{
    // token used to authentication to keycloak admin api
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = string.Empty;
}
