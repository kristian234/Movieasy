namespace Movieasy.Api.Controllers.Movies
{
    public sealed record UpdateMovieRequest(
        Guid MovieId,
        string Title,
        string Description,
        string TrailerUrl,
        int Rating,
        DateOnly? ReleaseDate,
        double Duration,
        List<Guid> Genres,
        List<Guid> Actors,
        IFormFile? Photo);
}
