using Bogus;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Domain.Movies;
using Movieasy.Infrastructure;

namespace Movieasy.Api.Extensions
{
    public static class SeedDataExtensions
    {
        public static void SeedData(this ApplicationDbContext app)
        {
            if (app.Movies.Any())
            {
                return;
            }

            var faker = new Faker();

            List<Movie> movies = new List<Movie>();

            for (int i = 0; i < 100; i++)
            {
                movies.Add(Movie.Create(
                    title: new Title(faker.Commerce.ProductName()),
                    description: new Description(faker.Commerce.ProductDescription()),
                    rating: (Rating)faker.PickRandom(1, 2, 3, 4, 5),
                    duration: Duration.Create(faker.Random.Double(1, 120)).Value,
                    uploadDate: faker.Date.Between(new DateTime(2012, 1, 1), new DateTime(2024, 12, 12)),
                    releaseDate: faker.Date.BetweenDateOnly(new DateOnly(2012, 1, 1), new DateOnly(2024, 12, 12))
                    )) ;
            }

            app.Movies.AddRange(movies);
            app.SaveChanges();
        }
    }
}
