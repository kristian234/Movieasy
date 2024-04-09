using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Actors.AddActor
{
    public sealed record AddActorCommand(
        string Name,
        string Biography) : ICommand<Guid>;
}
