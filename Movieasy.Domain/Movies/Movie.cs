using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Movies
{
    public sealed class Movie : Entity
    {
        private Movie(
            Guid id,
            Title title,
            Description description,
            Rating rating,
            Duration duration
            )
            : base(id)
        {
            Id = id;
            Title = title;
            Description = description;
            Rating = rating;
            Duration = duration;
        }

        public Title Title { get; private set; }

        public Description Description { get; private set; }

        public DateTime? ReleaseDate { get; internal set; }
        public Rating Rating { get; private set; }

        // TO DO: Remember to add Genres
        public Duration Duration { get; private set; }

        // TO DO: Remember to add the cast

        public static Movie Create(
            Title title,
            Description description,
            Rating rating,
            Duration duration)
        {
            Movie movie = new Movie(
                Guid.NewGuid(),
                title,
                description,
                rating,
                duration);

            return movie;
        }
    }


}
