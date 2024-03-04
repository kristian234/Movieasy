using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Movies.GetMovieById;

namespace Movieasy.Application.Movies.GetMovie
{
    public sealed record GetMoviesQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int Page,
        int PageSize) : IQuery<PagedList<MovieResponse>>;
}
