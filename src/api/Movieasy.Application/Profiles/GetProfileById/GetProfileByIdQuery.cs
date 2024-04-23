using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Profiles.GetProfileById
{
    public sealed record GetProfileByIdQuery(Guid UserId) : IQuery<ProfileResponse>;

}
