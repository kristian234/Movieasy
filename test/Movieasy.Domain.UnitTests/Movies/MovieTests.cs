using FluentAssertions;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Movies.Events;
using Movieasy.Domain.UnitTests.Infrastructure;

namespace Movieasy.Domain.UnitTests.Movies
{
    public class MovieTests : BaseTest
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

            // Assert
            movie.Title.Should().Be(MovieData.Title);
            movie.Description.Should().Be(MovieData.Description);
            movie.Rating.Should().Be(MovieData.Rating);
            movie.Trailer.Should().Be(MovieData.Trailer);
            movie.Duration.Should().Be(MovieData.Duration);
            movie.UploadDate.Should().Be(MovieData.UploadDate);
            movie.Photo.Should().Be(MovieData.Photo);
            movie.ReleaseDate.Should().Be(MovieData.ReleaseDate);
        }

        [Fact]
        public void Create_Should_RaiseMovieCreatedDomainEvent()
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

            // Assert 
            var domainEvent = AssertDomainEventWasPublished<MovieCreatedDomainEvent>(movie);

            domainEvent.MovieId.Should().Be(movie.Id);
        }

        [Fact]
        public void Update_ShouldSetValues()
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

            const string updatedValue = "TitleUpdated";
            const double updatedDuration = 2;
            DateOnly updatedReleaseDate = DateOnly.FromDateTime(new DateTime(2020, 2, 2, 2, 30, 0));
            const int updatedRating = 2;

            movie.Update(updatedValue, updatedValue, updatedRating, updatedDuration, updatedValue, updatedReleaseDate);

            // Assert
            movie.Title.Value.Should().Be(updatedValue);
            movie.Description.Value.Should().Be(updatedValue);
            movie.Trailer.Value.Should().Be(updatedValue);
            movie.Duration.Value.Should().Be(updatedDuration);

            movie.Rating.Should().Be((Rating)updatedRating);
            movie.UploadDate.Should().Be(MovieData.UploadDate);
            movie.Photo.Should().Be(MovieData.Photo);
            movie.ReleaseDate.Should().Be(updatedReleaseDate);
        }

        [Fact]
        public void Update_Should_Fail_If_DurationLessThanZero()
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

            const double updatedDuration = -10;

            Result updateResult = movie.Update(
                MovieData.Title.Value,
                MovieData.Description.Value,
                (int)MovieData.Rating,
                updatedDuration,
                MovieData.Trailer.Value,
                MovieData.ReleaseDate);

            // Assert
            updateResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void SetGenres_Should_SetValues()
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

            movie.SetGenres(MovieData.Genres);

            // Assert
            movie.Genres.Should().HaveCount(MovieData.Genres.Count());
        }
    }
}
