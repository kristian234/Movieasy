using Microsoft.AspNetCore.Http;
using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Movies.AddMovie
{
    public record AddMovieCommand(
        string Title,
        string Description,
        string TrailerUrl,
        int Rating,
        DateOnly? ReleaseDate,
        double Duration,
        List<Guid> Genres,
        IFormFile Photo) : ICommand<Guid>;
}
