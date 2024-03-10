using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Users.RefreshUser
{
    public sealed record RefreshUserCommand(string RefreshToken)
       : ICommand<RefreshTokenResponse>;
}
