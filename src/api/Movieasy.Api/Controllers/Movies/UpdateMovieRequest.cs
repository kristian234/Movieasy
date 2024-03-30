using Movieasy.Domain.Photos;

namespace Movieasy.Api.Controllers.Movies
{
    public sealed record UpdateMovieRequest(
        Guid MovieId,
        string Title,
        string Description,
        int Rating,
        DateOnly? ReleaseDate,
        double Duration,
        List<Guid> Genres,
        IFormFile? Photo);
}
