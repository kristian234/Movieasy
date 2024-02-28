using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Movies.AddMovie
{
    public record AddMovieCommand(
        string Title,
        string Description,
        int Rating,
        double Duration) : ICommand<Guid>;
}
