using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Movies.UpdateMovie
{
    public record UpdateMovieCommand(
        Guid MovieId,
        string Title,
        string Description,
        int Rating,
        double Duration) : ICommand;
}
