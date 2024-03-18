using Microsoft.AspNetCore.Http;
using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Movies.AddMovie
{
    public record AddMovieCommand(
        string Title,
        string Description,
        int Rating,
        DateOnly? ReleaseDate,
        double Duration,
        IFormFile Photo) : ICommand<Guid>;
}
