using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Reviews.Events;
using Movieasy.Domain.Users;

namespace Movieasy.Domain.Reviews
{
    public sealed class Review : Entity
    {
        private Review() { }

        private Review(
            Guid id,
            Movie movie,
            User user,
            DateTime createdOnUtc,
            Comment comment,
            Rating rating
            )
            : base(id)
        {
            Movie = movie;
            MovieId = movie.Id;
            User = user;
            UserId = user.Id;
            CreatedOnUtc = createdOnUtc;
            Comment = comment;
            Rating = rating;
        }

        public Guid MovieId { get; private set; }
        public Movie Movie { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

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
                movie,
                user,
                createdOnUtc,
                comment,
                rating);

            review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

            return review;
        }

        public Result Update(string comment, int rating)
        {
            Comment newComment = new Comment(comment);

            Result<Rating> newRatingResult = Rating.Create(rating);
            if (newRatingResult.IsFailure)
            {
                return Result.Failure(Rating.Invalid);
            }

            Comment = newComment;
            Rating = newRatingResult.Value;

            return Result.Success();
        }
    }
}
