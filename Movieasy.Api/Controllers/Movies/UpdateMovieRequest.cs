namespace Movieasy.Api.Controllers.Movies
{
    public sealed record UpdateMovieRequest(
        Guid MovieId,
        string Title,
        string Description,
        int Rating,
        DateOnly? ReleaseDate,
        double Duration);
}
