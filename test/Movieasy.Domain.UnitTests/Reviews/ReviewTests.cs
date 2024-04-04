﻿using FluentAssertions;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Reviews;
using Movieasy.Domain.Reviews.Events;
using Movieasy.Domain.UnitTests.Infrastructure;
using Movieasy.Domain.Users;

namespace Movieasy.Domain.UnitTests.Reviews
{
    public class ReviewTests : BaseTest
    {
        [Fact]
        public void Create_Should_SetValues()
        {
            // Act
            Movie movie = Movie.Create(
              MovieData.Title,
              MovieData.Description,
              MovieData.Rating,
              MovieData.Trailer,
              MovieData.Duration,
              MovieData.UploadDate,
              MovieData.Photo,
              MovieData.ReleaseDate);

            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);
            Review review = Review.Create(movie, user, ReviewData.Rating, ReviewData.Comment, ReviewData.CreatedOnDate).Value;

            // Assert
            review.UserId.Should().Be(user.Id);
            review.MovieId.Should().Be(movie.Id);
            review.User.Should().Be(user);

            review.Rating.Should().Be(ReviewData.Rating);
            review.Comment.Should().Be(ReviewData.Comment);
            review.CreatedOnUtc.Should().Be(ReviewData.CreatedOnDate);
        }

        [Fact]
        public void Create_Should_Fail_If_MovieHasNoReleaseDate()
        {
            // Act
            Movie movie = Movie.Create(
              MovieData.Title,
              MovieData.Description,
              MovieData.Rating,
              MovieData.Trailer,
              MovieData.Duration,
              MovieData.UploadDate,
              MovieData.Photo,
              null);

            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

            Result reviewResult = Review.Create(movie, user, ReviewData.Rating, ReviewData.Comment, ReviewData.CreatedOnDate);

            // Assert
            reviewResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Create_Should_RaiseReviewCreatedDomainEvent()
        {
            // Act
            Movie movie = Movie.Create(
             MovieData.Title,
             MovieData.Description,
             MovieData.Rating,
             MovieData.Trailer,
             MovieData.Duration,
             MovieData.UploadDate,
             MovieData.Photo,
             MovieData.ReleaseDate);

            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);
            Review review = Review.Create(movie, user, ReviewData.Rating, ReviewData.Comment, ReviewData.CreatedOnDate).Value;

            // Assert
            var domainEvent = AssertDomainEventWasPublished<ReviewCreatedDomainEvent>(review);

            domainEvent.ReviewId.Should().Be(review.Id);
        }
    }
}
