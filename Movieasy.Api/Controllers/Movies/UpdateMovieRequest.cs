namespace Movieasy.Api.Controllers.Movies
{
    public sealed record UpdateMovieRequest(
        Guid MovieId,
        string Title,
        string Description,
        int Rating,
        double Duration);
}
