namespace Movieasy.Application.Users.LoginUser
{
    public sealed record JwtServiceResult(
        string AccessToken,
        string RefreshToken);
}
