using Microsoft.AspNetCore.Http;
using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Movies.UpdateMovie
{
    public record UpdateMovieCommand(
        Guid MovieId,
        string Title,
        string Description,
        int Rating,
        DateOnly? ReleaseDate,
        double Duration,
        IFormFile? Photo) : ICommand;
}
