using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Movies
{
    public sealed class Movie : Entity
    {
        public Movie(
            Guid id,
            Title title,
            Description description,
            Rating rating,
            Duration duration
            )
            : base(id)
        {

        }

        public Title Title { get; private set; }

        public Description Description { get; private set; }

        public DateTime? ReleaseDate { get; private set; }

        public Rating Rating { get; private set; }

        // TO DO: Remember to add Genres
        public Duration Duration { get; private set; }

        // TO DO: Remember to add the cast
    }
}
