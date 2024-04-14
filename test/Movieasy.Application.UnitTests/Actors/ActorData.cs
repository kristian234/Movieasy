using Movieasy.Domain.Movies;

namespace Movieasy.Application.UnitTests.Actors
{
    internal static class ActorData
    {
        public static readonly string Name = "Johnny Depp";
        public static readonly string Biography = "Cool Biography";

        public static readonly Movie Movie = Movie.Create(
            new Title("Test movie"),
            new Description("Test description"),
            Rating.G,
            new Trailer("Test trailer"),
            Duration.Create(42).Value,
            DateTime.UtcNow,
            MovieData.Photo,
            MovieData.ReleaseDate);
    }
}
