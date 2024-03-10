namespace Movieasy.Application.Users.LoginUser
{
    public sealed record AccessTokenResponse(string AccessToken, string? RefreshToken);
}
