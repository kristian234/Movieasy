using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Actors
{
    public static class ActorErrors
    {
        public static Error NotFound = new Error(
            "Actor.NotFound",
            "The actor with the specified identifier was not found");
    }
}
