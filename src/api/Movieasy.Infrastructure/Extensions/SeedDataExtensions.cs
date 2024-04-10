using Movieasy.Domain.Actors;
using Movieasy.Domain.Genres;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;
using Movieasy.Domain.Users;
using Movieasy.Infrastructure;
using Newtonsoft.Json;

namespace Movieasy.Api.Extensions
{
    public class MovieData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Trailer { get; set; }
        public double Duration { get; set; }
        public PhotoData Photo { get; set; }
        public string UploadDate { get; set; }
        public string ReleaseDate { get; set; }
        public List<string> Genres { get; set; }
        public List<ActorData>? Actors { get; set; }
    }

    public class ActorData
    {
        public string Name { get; set; }
        public string Biography { get; set; } = string.Empty;
    }

    public class PhotoData
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string AltText { get; set; }
    }

    public static class SeedDataExtensions
    {
        public static void SeedData(this ApplicationDbContext app)
        {
            if (app.Movies.Any() == false)
            {
                string path = "/app/Extensions/seed.json";
                string jsonData = File.ReadAllText(path);

                var moviesData = JsonConvert.DeserializeObject<List<MovieData>>(jsonData);

                foreach (var movieData in moviesData)
                {
                    var title = new Title(movieData.Title);
                    var description = new Description(movieData.Description);
                    var rating = (Rating)movieData.Rating;
                    var trailer = new Trailer(movieData.Trailer);
                    var duration = Duration.Create(movieData.Duration);
                    var uploadDate = DateTime.Parse(movieData.UploadDate).ToUniversalTime();
                    DateOnly? releaseDate = null;
                    if (movieData.ReleaseDate != null)
                    {
                        releaseDate = DateOnly.Parse(movieData.ReleaseDate);
                    }

                    var photoData = movieData.Photo;
                    var photo = Photo.Create(new PublicId(photoData.Id), new Url(photoData.Url));

                    app.Photos.Add(photo);


                    List<Genre> genres = new List<Genre>();
                    foreach (string genreName in movieData.Genres)
                    {
                        var genre = app.Genres.FirstOrDefault(g => ((string)g.Name) == genreName);
                        if (genre == null)
                        {
                            genre = Genre.Create(new Domain.Genres.Name(genreName));
                            app.Genres.Add(genre);
                            app.SaveChanges();
                        }
                        genres.Add(genre);
                    }

                    List<Actor> actors = new List<Actor>();
                    if (movieData.Actors != null)
                    {
                        foreach (var actor in movieData.Actors)
                        {
                            var existingActor = app.Actors.FirstOrDefault(a => ((string)a.Name) == actor.Name);
                            if (existingActor == null)
                            {
                                existingActor = Actor.Create(new Domain.Actors.Name(actor.Name), new Biography(actor.Biography));
                                app.Actors.Add(existingActor);
                                app.SaveChanges();
                            }
                            actors.Add(existingActor);
                        }
                    }

                    Movie movie = Movie.Create(title, description, rating, trailer, duration.Value, uploadDate, photo, releaseDate);
                    movie.SetGenres(genres);

                    if(movieData.Actors != null)
                    {
                        movie.SetCast(actors);
                    }

                    app.Movies.Add(movie);
                }

                app.Attach(Role.Admin);
                app.Attach(Role.Registered);
            }

            if (app.Users.Any() == false)
            {

                var user = User.Create(
                    new FirstName("admin"),
                    new LastName("admin"),
                    new Email("admin@gmail.com"));

                user.AddUserRole(Role.Admin);
                user.SetIdentityId("3538994f-b095-45f6-8f98-a95dd61b84c2");

                app.Users.Add(user);
            }
            //app.Photos.AddRange(photos);
            //app.Movies.AddRange(movies);
            app.SaveChanges();
        }
    }
}
