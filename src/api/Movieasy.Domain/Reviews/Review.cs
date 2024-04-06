﻿using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Reviews.Events;
using Movieasy.Domain.Users;

namespace Movieasy.Domain.Reviews
{
    public sealed class Review : Entity
    {
        private Review(Guid id) : base(id) { }

        private Review(
            Guid id,
            Guid movieId,
            User user,
            DateTime createdOnUtc,
            Comment comment,
            Rating rating
            )
            : base(id)
        {
            MovieId = movieId;
            User = user;
            UserId = user.Id;
            CreatedOnUtc = createdOnUtc;
            Comment = comment;
            Rating = rating;
        }

        public Guid MovieId { get; private set; }

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
                movie.Id,
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
