using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Movieasy.Application.Abstractions.Photos;
using Movieasy.Application.Movies.DeleteMovie;
using Movieasy.Application.Movies.UpdateMovie;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Actors;
using Movieasy.Domain.Genres;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Movies
{
    public class UpdateMovieTests
    {
        private readonly UpdateMovieCommand Command;
        private readonly UpdateMovieCommandHandler _handler;

        private readonly IFormFile _formFileMock = Substitute.For<IFormFile>();
        private readonly IMovieRepository _movieRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IPhotoAccessor _photoAccessorMock;
        private readonly IGenreRepository _genreRepositoryMock;
        private readonly IActorRepository _actorRepositoryMock;

        public UpdateMovieTests()
        {
            _movieRepositoryMock = Substitute.For<IMovieRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _photoAccessorMock = Substitute.For<IPhotoAccessor>();
            _genreRepositoryMock = Substitute.For<IGenreRepository>();
            _actorRepositoryMock = Substitute.For<IActorRepository>();

            Command = new UpdateMovieCommand(
                Guid.NewGuid(),
                MovieData.Title,
                MovieData.Description,
                MovieData.TrailerUrl,
                MovieData.Rating,
                MovieData.ReleaseDate,
                MovieData.Duration,
                MovieData.GenreIds,
                MovieData.ActorIds,
                _formFileMock);

            _handler = new UpdateMovieCommandHandler(
                _movieRepositoryMock,
                _unitOfWorkMock,
                _photoAccessorMock,
                _genreRepositoryMock,
                _actorRepositoryMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenMovieNotFound()
        {
            // Arrange
            _movieRepositoryMock
                .GetByIdAsync(Command.MovieId, Arg.Any<CancellationToken>())
                .Returns((Movie?)null);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(MovieErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenGenresNotFound()
        {
            // Arrange
            _movieRepositoryMock
                .GetByIdAsync(Command.MovieId, Arg.Any<CancellationToken>())
                .Returns(MovieData.Movie);

            _genreRepositoryMock
                .GetByIdsAsync(Command.Genres, Arg.Any<CancellationToken>())
                .Returns(new List<Genre>());

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(GenreErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenActorsNotFound()
        {
            // Arrange
            _movieRepositoryMock
                .GetByIdAsync(Command.MovieId, Arg.Any<CancellationToken>())
                .Returns(MovieData.Movie);

            _genreRepositoryMock
                .GetByIdsAsync(Command.Genres, Arg.Any<CancellationToken>())
                .Returns(MovieData.Genres);

            _actorRepositoryMock
                .GetByIdsAsync(Command.Actors, Arg.Any<CancellationToken>())
                .Returns(new List<Actor>());

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(ActorErrors.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenPhotoUploadFails()
        {
            // Arrange
            _movieRepositoryMock
                .GetByIdAsync(Command.MovieId, Arg.Any<CancellationToken>())
                .Returns(MovieData.Movie);

            _genreRepositoryMock
                .GetByIdsAsync(Command.Genres, Arg.Any<CancellationToken>())
                .Returns(MovieData.Genres);

            _actorRepositoryMock
                .GetByIdsAsync(Command.Actors, Arg.Any<CancellationToken>())
                .Returns(MovieData.Actors);

            _photoAccessorMock
                .AddPhotoAsync(_formFileMock)
                .Returns(Result.Failure<PhotoUploadResult>(new Error("Test", "Test")));

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenPhotoDeleteFails()
        {
            // Arrange
            _movieRepositoryMock
                .GetByIdAsync(Command.MovieId, Arg.Any<CancellationToken>())
                .Returns(MovieData.Movie);

            _genreRepositoryMock
                .GetByIdsAsync(Command.Genres, Arg.Any<CancellationToken>())
                .Returns(MovieData.Genres);

            _actorRepositoryMock
                .GetByIdsAsync(Command.Actors, Arg.Any<CancellationToken>())
                .Returns(MovieData.Actors);

            _photoAccessorMock
                .AddPhotoAsync(_formFileMock)
                .Returns(Result.Success<PhotoUploadResult>(
                    new PhotoUploadResult(MovieData.Photo.PublicId.Value, MovieData.Photo.Url.Value)));

            _photoAccessorMock
                .DeletePhotoAsync(MovieData.Photo.PublicId.Value)
                .Returns(false);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(MovieErrors.UpdateFailed);
        }
    }
}
