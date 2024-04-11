using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Actors.GetActorById
{
    public record GetActorByIdQuery(
        Guid ActorId) : IQuery<ActorResponse>;
}
