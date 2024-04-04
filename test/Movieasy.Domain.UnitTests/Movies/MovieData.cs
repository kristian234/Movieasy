using Movieasy.Domain.Genres;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;

internal static class MovieData
{
    public static readonly Description Description = new("Movie description");
    public static readonly Duration Duration = Duration.Create(43).Value;
    public static readonly Rating Rating = Rating.G;
    public static readonly Title Title = new("Movie title");
    public static readonly Trailer Trailer = new("Movie trailer URL");
    public static readonly DateTime UploadDate = new DateTime(2023, 6, 15, 14, 30, 0);
    public static readonly DateOnly ReleaseDate = DateOnly.FromDateTime(new DateTime(2023, 6, 15, 14, 30, 0));

    // Movie photo
    public static readonly Photo Photo = Photo.Create(new PublicId("testId"), new Url("testUrl"));

    public static readonly IEnumerable<Genre> Genres = new List<Genre>()
    {
        Genre.Create(new Name("Action")),
        Genre.Create(new Name("Comedy")),
        Genre.Create(new Name("Fantasy")),
    };
}