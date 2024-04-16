using Movieasy.Domain.Actors;

namespace Movieasy.Application.IntegrationTests.Actors
{
    internal static class ActorData
    {
        public static readonly List<Actor> Actors = new List<Actor>()
        {
            Actor.Create(new Name("Johnny Depp"), new Biography("Cool biography")),
            Actor.Create(new Name("Rowan Atkinson"), new Biography("Cool biography2"))
        };
    }
}
