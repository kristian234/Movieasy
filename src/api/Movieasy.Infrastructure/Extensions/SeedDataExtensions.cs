using Bogus;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;
using Movieasy.Domain.Users;
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

            List<Photo> photos = new List<Photo>();
            List<Movie> movies = new List<Movie>();

            for (int i = 0; i < 100; i++)
            {
                var photo = Photo.Create(
                    new PublicId("xpgqeq4xjlwchheca3ul"),
                    new Url("https://res.cloudinary.com/dpfb4wi7z/image/upload/v1710787080/xpgqeq4xjlwchheca3ul.jpg"));

                photos.Add(photo);

                movies.Add(Movie.Create(
                    title: new Title(faker.Commerce.ProductName()),
                    description: new Description(faker.Commerce.ProductDescription()),
                    rating: (Rating)faker.PickRandom(1, 2, 3, 4, 5),
                    duration: Duration.Create(faker.Random.Double(1, 120)).Value,
                    uploadDate: faker.Date.Between(new DateTime(2012, 1, 1).ToUniversalTime(), new DateTime(2024, 12, 12).ToUniversalTime()),
                    releaseDate: faker.Date.BetweenDateOnly(new DateOnly(2012, 1, 1), new DateOnly(2024, 12, 12)),
                    photo: photo,
                    trailer: new Trailer("https://www.youtube.com/watch?v=mqqft2x_Aa4")
                    ));
            }

            app.Attach(Role.Admin);
            app.Attach(Role.Registered);

            var user = User.Create(
                new FirstName("kristian"),
                new LastName("teodosiev"),
                new Email("kristian@gmail.com"));

            user.AddUserRole(Role.Admin);
            user.SetIdentityId("3538994f-b095-45f6-8f98-a95dd61b84c2");

            app.Users.Add(user);

            app.Photos.AddRange(photos);
            app.Movies.AddRange(movies);
            app.SaveChanges();
        }
    }
}
