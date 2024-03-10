using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Users.GetLoggedInUser
{
    public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
}
