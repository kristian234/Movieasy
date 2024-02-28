namespace Movieasy.Api.Controllers.Movies
{
    public sealed record AddMovieRequest(
        string Title,
        string Description,
        int Rating,
        double Duration);
}
