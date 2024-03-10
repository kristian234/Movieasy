using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Users.LoginUser
{
    public sealed record LogInUserCommand(string Email, string Password, bool RememberMe = false) 
        : ICommand<AccessTokenResponse>;
}
