namespace Movieasy.Api.Controllers.Actors
{
    public record UpdateActorRequest(
        Guid ActorId,
        string Name,
        string Biography);
}
