using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Reviews;

namespace Movieasy.Domain.Movies
{
    public sealed class Movie : Entity
    {
        private Movie(
            Guid id,
            Title title,
            Description description,
            Rating rating,
            Duration duration,
            DateOnly? releaseDate
            )
            : base(id)
        {
            Id = id;
            Title = title;
            Description = description;
            Rating = rating;
            Duration = duration;
            ReleaseDate = releaseDate;
        }

        public Title Title { get; private set; }

        public Description Description { get; private set; }

        public DateOnly? ReleaseDate { get; internal set; }
        public Rating Rating { get; private set; }

        // TO DO: Remember to add Genres
        public Duration Duration { get; private set; }
        public DateTime UploadDate { get; internal set; }

        // TO DO: Remember to add the cast

        public static Movie Create(
            Title title,
            Description description,
            Rating rating,
            Duration duration,
            DateTime uploadDate,
            DateOnly? releaseDate)
        {
            Movie movie = new Movie(
                Guid.NewGuid(),
                title,
                description,
                rating,
                duration,
                releaseDate);

            return movie;
        }

        public Result Update(string title, string description, int rating, double duration, DateOnly? releaseDate)
        {
            Result<Duration> newDuration = Duration.Create(duration);
            if (newDuration.IsFailure)
            {
                return Result.Failure(Duration.Invalid);
            }

            Title newTitle = new Title(title);
            Description newDescription = new Description(description);
            Rating newRating = (Rating)rating;

            Title = newTitle;
            Description = newDescription;
            Rating = newRating;
            Duration = newDuration.Value;
            ReleaseDate = releaseDate;

            return Result.Success();
        }
    }
}
