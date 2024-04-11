using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Actors.GetActorById
{
    public sealed record GetActorByIdQuery(
        Guid ActorId) : IQuery<ActorResponse>;
}
