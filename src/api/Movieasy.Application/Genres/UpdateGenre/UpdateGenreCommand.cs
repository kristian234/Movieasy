using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Genres.UpdateGenre
{
    public sealed record UpdateGenreCommand(
        Guid GenreId,
        string Name) : ICommand;
}
