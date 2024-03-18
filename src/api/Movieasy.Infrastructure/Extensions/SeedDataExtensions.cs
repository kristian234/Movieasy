using Bogus;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;
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

            //if (app.Photos.Any() == false)
            //{
            //    app.Photos.Add(Photo.Create(new PublicId("null"), new Url("null")));
            //    app.SaveChanges();
            //}

            var faker = new Faker();
            var photo = app.Photos.FirstOrDefault(x => (string)x.Url == "null");

            List<Movie> movies = new List<Movie>();

            //for (int i = 0; i < 100; i++)
            //{
            //    movies.Add(Movie.Create(
            //        title: new Title(faker.Commerce.ProductName()),
            //        description: new Description(faker.Commerce.ProductDescription()),
            //        rating: (Rating)faker.PickRandom(1, 2, 3, 4, 5),
            //        duration: Duration.Create(faker.Random.Double(1, 120)).Value,
            //        uploadDate: faker.Date.Between(new DateTime(2012, 1, 1).ToUniversalTime(), new DateTime(2024, 12, 12).ToUniversalTime()),
            //        releaseDate: faker.Date.BetweenDateOnly(new DateOnly(2012, 1, 1), new DateOnly(2024, 12, 12)),
            //        photo: photo
            //        )) ;
            //}

            //app.Movies.AddRange(movies);
            app.SaveChanges();
        }
    }
}
