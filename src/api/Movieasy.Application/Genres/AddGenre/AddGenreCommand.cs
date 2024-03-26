using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Genres.AddGenre
{
    public record AddGenreCommand(
        string Name) : ICommand<Guid>;
}
