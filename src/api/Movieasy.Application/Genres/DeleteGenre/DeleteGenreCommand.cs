
using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Genres.DeleteGenre
{
    public record DeleteGenreCommand(Guid GenreId) : ICommand;
}
