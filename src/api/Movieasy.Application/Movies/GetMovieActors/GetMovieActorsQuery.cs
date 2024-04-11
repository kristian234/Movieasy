using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Actors.GetActorById;

namespace Movieasy.Application.Movies.GetMovieActorsQuery
{
    public sealed record GetMovieActorsQuery(
        Guid MovieId) : IQuery<IEnumerable<ActorResponse>>;
}
