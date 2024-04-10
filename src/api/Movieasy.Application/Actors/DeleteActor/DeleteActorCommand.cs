using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Actors.DeleteActor
{
    public sealed record DeleteActorCommand(Guid ActorId) : ICommand;
}
