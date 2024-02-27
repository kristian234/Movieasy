using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Movies;

namespace Movieasy.Application.Movies.AddMovie
{
    public record AddMovieCommand(
        Guid id,
        string title,
        string description,
        int rating,
        double duration) : ICommand<Guid>;
}
