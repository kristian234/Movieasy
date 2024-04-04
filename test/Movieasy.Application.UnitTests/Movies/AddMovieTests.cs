using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Movieasy.Application.Abstractions.Clock;
using Movieasy.Application.Abstractions.Photos;
using Movieasy.Application.Movies.AddMovie;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Genres;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Movies
{
    public class AddMovieTests
    {
        private static readonly DateTime UtcNow = DateTime.UtcNow;
        private readonly AddMovieCommand Command;

        private readonly AddMovieCommandHandler _handler;
        private readonly IFormFile _formFileMock = Substitute.For<IFormFile>();
        private readonly IMovieRepository _movieRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IDateTimeProvider _dateTimeProviderMock;
        private readonly IPhotoAccessor _photoAccessorMock;
        private readonly IPhotoRepository _photoRepositoryMock;
        private readonly IGenreRepository _genreRepositoryMock;


        public AddMovieTests()
        {
            _formFileMock = Substitute.For<IFormFile>();
            _movieRepositoryMock = Substitute.For<IMovieRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _photoAccessorMock = Substitute.For<IPhotoAccessor>();
            _photoRepositoryMock = Substitute.For<IPhotoRepository>();
            _genreRepositoryMock = Substitute.For<IGenreRepository>();

            _dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
            _dateTimeProviderMock.UtcNow.Returns(UtcNow);

            Command = new AddMovieCommand(
                MovieData.Title,
                MovieData.Description,
                MovieData.TrailerUrl,
                MovieData.Rating,
                MovieData.ReleaseDate,
                MovieData.Duration,
                MovieData.GenreIds,
                _formFileMock);

            _handler = new AddMovieCommandHandler(
                _movieRepositoryMock,
                _unitOfWorkMock,
                _dateTimeProviderMock,
                _photoAccessorMock,
                _photoRepositoryMock,
                _genreRepositoryMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenGenresNotFound()
        {
            // Arrange
            _genreRepositoryMock
                .GetByIdsAsync(Command.Genres, Arg.Any<CancellationToken>())
                .Returns(new List<Genre>());

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert 
            result.Error.Should().Be(GenreErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenPhotoUploadFails()
        {
            // Arrange
            _genreRepositoryMock
                .GetByIdsAsync(Command.Genres, Arg.Any<CancellationToken>())
                .Returns(MovieData.Genres);


            _photoAccessorMock
                .AddPhotoAsync(Command.Photo)
                .Returns(Result.Failure<PhotoUploadResult>(PhotoErrors.UploadingImageFailed));

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(PhotoErrors.UploadingImageFailed);
        }

    }
}
