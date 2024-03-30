using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Genres.UpdateGenre
{
    public record UpdateGenreCommand(
        Guid GenreId,
        string Name) : ICommand;
}
