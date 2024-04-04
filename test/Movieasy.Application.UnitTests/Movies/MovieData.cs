using Movieasy.Domain.Genres;
using Movieasy.Domain.Photos;

internal static class MovieData
{
    public static readonly string Title = "MyTitle";
    public static readonly string Description = "MyDescription";
    public static readonly string TrailerUrl = "MyTrailerUrl";
    public static readonly int Rating = 2;
    public static readonly DateOnly ReleaseDate = DateOnly.FromDateTime(new DateTime(2023, 6, 15, 14, 30, 0));
    public static readonly double Duration = 44;

    public static readonly Photo Photo = Photo.Create(new PublicId("testPublicId"), new Url("testUrl"));

    public static readonly List<Genre> Genres = new List<Genre>()
    {
        Genre.Create(new Name("Action")),
        Genre.Create(new Name("Fantasy"))
    };

    public static readonly List<Guid> GenreIds = new List<Guid>()
    {
        Genres[0].Id,
        Genres[1].Id,
    };
}