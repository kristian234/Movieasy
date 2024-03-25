using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Movies.DeleteMovie
{
    public record DeleteMovieCommand(Guid MovieId) : ICommand;
}
