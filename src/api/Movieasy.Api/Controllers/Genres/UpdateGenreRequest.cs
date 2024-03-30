namespace Movieasy.Api.Controllers.Genres
{
    public sealed record UpdateGenreRequest(
        Guid GenreId,
        string Name);
}
