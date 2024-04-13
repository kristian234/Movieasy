using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Genres.AddGenre
{
    public sealed record AddGenreCommand(
        string Name) : ICommand<Guid>;
}
