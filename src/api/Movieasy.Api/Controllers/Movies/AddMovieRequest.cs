namespace Movieasy.Api.Controllers.Movies
{
    public sealed record AddMovieRequest(
        string Title,
        string Description,
        int Rating,
        DateOnly? ReleaseDate,
        double Duration,
        List<Guid> Genres,
        IFormFile Photo);
}
