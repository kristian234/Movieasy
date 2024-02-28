using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Reviews.Events;
using Movieasy.Domain.Users;

namespace Movieasy.Domain.Reviews
{
    public sealed class Review : Entity
    {
        private Review(
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

        public static Result<Review> Create(
            Movie movie,
            User user,
            Rating rating,
            Comment comment,
            DateTime createdOnUtc
            )
        {
            if (!movie.ReleaseDate.HasValue)
            {
                return Result.Failure<Review>(ReviewErrors.NotEligible);
            }

            Review review = new Review(
                Guid.NewGuid(),
                movie.Id,
                user.Id,
                createdOnUtc,
                comment,
                rating);

            review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

            return review;
        }
    }
}
