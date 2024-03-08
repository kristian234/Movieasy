using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Users.RegisterUser
{
    public sealed record RegisterUserCommand(
        string Email,
        string FirstName,
        string LastName,
        string Password) : ICommand<Guid>;
}
