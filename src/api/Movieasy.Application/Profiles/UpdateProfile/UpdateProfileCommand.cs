using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Profiles.UpdateProfile
{
    public sealed record UpdateProfileCommand(
        Guid UserId,
        string Details) : ICommand;
}
