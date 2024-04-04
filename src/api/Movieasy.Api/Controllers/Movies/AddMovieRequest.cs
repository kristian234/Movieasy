namespace Movieasy.Api.Controllers.Movies
{
    public sealed record AddMovieRequest(
        string Title,
        string Description,
        string TrailerUrl,
        int Rating,
        DateOnly? ReleaseDate,
        double Duration,
        List<Guid> Genres,
        IFormFile Photo);
}
