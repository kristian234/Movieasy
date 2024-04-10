using Microsoft.AspNetCore.Http;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Actors;

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
        List<Guid> Actors,
        IFormFile Photo) : ICommand<Guid>;
}
