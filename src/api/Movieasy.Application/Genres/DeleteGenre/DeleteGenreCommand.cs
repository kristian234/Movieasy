
using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Genres.DeleteGenre
{
    public sealed record DeleteGenreCommand(Guid GenreId) : ICommand;
}
