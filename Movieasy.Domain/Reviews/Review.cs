using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Reviews
{
    public sealed class Review : Entity
    {
        public Review(
            Guid id,
            Guid movieId,
            Guid userId,
            DateTime createdOnUtc,
            Comment comment,
            Rating rating
            )
            : base(id)
        {
            MovieId = movieId;
            UserId = userId;
            CreatedOnUtc = createdOnUtc;
            Comment = comment;
            Rating = rating;
        }

        public Guid MovieId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public Comment Comment { get; private set; }
        public Rating Rating { get; private set; }
    }
}
