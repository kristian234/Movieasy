
using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Actors.UpdateActor
{
    public sealed record UpdateActorCommand(
        Guid ActorId,
        string Name,
        string Biography) : ICommand;
}
