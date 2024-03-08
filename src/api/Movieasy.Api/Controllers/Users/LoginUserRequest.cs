namespace Movieasy.Api.Controllers.Users
{
    public sealed record LoginUserRequest(
       string Email,
       string Password);
}
