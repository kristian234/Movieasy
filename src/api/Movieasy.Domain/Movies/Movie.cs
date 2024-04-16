using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Actors;
using Movieasy.Domain.Genres;
using Movieasy.Domain.Movies.Events;
using Movieasy.Domain.Photos;

namespace Movieasy.Domain.Movies
{
    public sealed class Movie : Entity
    {
        private Movie() { }

        private Movie(
            Guid id,
            Photo photo,
            Title title,
            Description description,
            Trailer trailer,
            Rating rating,
            Duration duration,
            DateTime uploadDate,
            DateOnly? releaseDate
            )
            : base(id)
        {
            Id = id;
            Title = title;
            Description = description;
            Rating = rating;
            Duration = duration;
            UploadDate = uploadDate;
            PhotoId = photo.Id;
            Photo = photo;
            ReleaseDate = releaseDate;
            Trailer = trailer;
        }

        public Title Title { get; private set; }
        public Description Description { get; private set; }

        public Guid PhotoId { get; private set; }
        public Photo Photo { get; private set; } = null!;

        public DateOnly? ReleaseDate { get; internal set; }
        public Rating Rating { get; private set; }
        public Trailer Trailer { get; private set; }

        private List<Genre> _genres = new List<Genre>();
        public IReadOnlyCollection<Genre> Genres => _genres.AsReadOnly();

        public Duration Duration { get; private set; }
        public DateTime UploadDate { get; internal set; }

        public List<Actor> _actors = new List<Actor>();
        public IReadOnlyCollection<Actor> Actors => _actors.AsReadOnly();

        public static Movie Create(
            Title title,
            Description description,
            Rating rating,
            Trailer trailer,
            Duration duration,
            DateTime uploadDate,
            Photo photo,
            DateOnly? releaseDate)
        {
            Movie movie = new Movie(
                Guid.NewGuid(),
                photo,
                title,
                description,
                trailer,
                rating,
                duration,
                uploadDate,
                releaseDate);

            movie.RaiseDomainEvent(new MovieCreatedDomainEvent(movie.Id));

            return movie;
        }

        public Result Update(string title, string description, int rating, double duration, string trailer, DateOnly? releaseDate)
        {
            Result<Duration> newDuration = Duration.Create(duration);
            if (newDuration.IsFailure)
            {
                return Result.Failure(Duration.Invalid);
            }

            Title newTitle = new Title(title);
            Description newDescription = new Description(description);
            Rating newRating = (Rating)rating;
            Trailer newTrailer = new Trailer(trailer);

            Title = newTitle;
            Description = newDescription;
            Rating = newRating;
            Duration = newDuration.Value;
            ReleaseDate = releaseDate;
            Trailer = newTrailer;

            return Result.Success();
        }

        public Result SetGenres(IEnumerable<Genre> genres)
        {
            _genres = genres.ToList();

            return Result.Success();
        }

        public Result SetCast(IEnumerable<Actor> actors)
        {
            _actors = actors.ToList();

            return Result.Success();
        }
    }
}
