using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Movies.GetMovie
{
    public sealed record GetMovieQuery(Guid MovieId) : IQuery<MovieResponse>;
}
