using FluentAssertions;
using Movieasy.Application.Abstractions.Photos;
using Movieasy.Application.Movies.AddMovie;
using Movieasy.Application.Movies.DeleteMovie;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Movies
{
    public class DeleteMovieTests
    {
        private readonly DeleteMovieCommand Command;
        private readonly DeleteMovieCommandHandler _handler;

        private readonly IMovieRepository _movieRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IPhotoAccessor _photoAccessorMock;
        private readonly IPhotoRepository _photoRepositoryMock;

        public DeleteMovieTests()
        {
            _movieRepositoryMock = Substitute.For<IMovieRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _photoAccessorMock = Substitute.For<IPhotoAccessor>();
            _photoRepositoryMock = Substitute.For<IPhotoRepository>();

            Command = new DeleteMovieCommand(
                Guid.NewGuid());

            _handler = new DeleteMovieCommandHandler(
                _movieRepositoryMock,
                _unitOfWorkMock,
                _photoRepositoryMock,
                _photoAccessorMock);
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
        public async Task Handle_Should_ReturnFailure_WhenDeletePhotoFails()
        {
            // Arrange
            _movieRepositoryMock
                .GetByIdAsync(Command.MovieId, Arg.Any<CancellationToken>())
                .Returns(MovieData.Movie);

            _photoAccessorMock
                .DeletePhotoAsync(MovieData.Movie.Photo.PublicId.Value)
                .Returns(false);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(MovieErrors.DeleteFailed);
        }

    }
}
