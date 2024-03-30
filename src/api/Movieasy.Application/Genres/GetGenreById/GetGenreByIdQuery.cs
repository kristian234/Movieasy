using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Genres.GetGenre;

namespace Movieasy.Application.Genres.GetGenreById
{
    public sealed record GetGenreByIdQuery(Guid GenreId) : IQuery<GenreResponse>;
}
