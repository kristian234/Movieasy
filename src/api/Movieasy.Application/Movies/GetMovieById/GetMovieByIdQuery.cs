using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Movies.GetMovieById
{
    public sealed record GetMovieByIdQuery(Guid MovieId) : IQuery<MovieResponse>;
}
