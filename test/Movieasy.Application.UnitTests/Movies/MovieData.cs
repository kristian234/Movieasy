using Movieasy.Domain.Actors;
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
        Genre.Create(new Movieasy.Domain.Genres.Name("Action")),
        Genre.Create(new Movieasy.Domain.Genres.Name("Fantasy"))
    };

    public static readonly List<Guid> GenreIds = new List<Guid>()
    {
        Genres[0].Id,
        Genres[1].Id,
    };


    public static readonly List<Actor> Actors = new List<Actor>()
    {
        Actor.Create(new Movieasy.Domain.Actors.Name("Johnny Depp"), new Biography("Cool biography")),
        Actor.Create(new Movieasy.Domain.Actors.Name("Rowan Atkinson"), new Biography("Cool biography2"))
    };

    public static readonly List<Guid> ActorIds = new List<Guid>()
    {
        Actors[0].Id,
        Actors[1].Id,
    };
}