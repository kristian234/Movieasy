using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Genres.GetGenre
{
    public sealed record GetGenresQuery() : IQuery<IEnumerable<GenreResponse>>;
}
