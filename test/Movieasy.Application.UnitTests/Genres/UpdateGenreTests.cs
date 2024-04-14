using FluentAssertions;
using Movieasy.Application.Genres.UpdateGenre;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Genres;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Genres
{
    public class UpdateGenreTests
    {
        private readonly UpdateGenreCommand Command;
        private readonly UpdateGenreCommandHandler _handler;

        private readonly IGenreRepository _genreRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public UpdateGenreTests()
        {
            _genreRepositoryMock = Substitute.For<IGenreRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            Command = new UpdateGenreCommand(
                Guid.NewGuid(),
                GenreData.Name);

            _handler = new UpdateGenreCommandHandler(
                _genreRepositoryMock,
                _unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenGenreNotFound()
        {
            // Arrange
            _genreRepositoryMock
                .GetByIdAsync(Command.GenreId, Arg.Any<CancellationToken>())
                .Returns((Genre?)null);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(GenreErrors.NotFound);
        }
    }
}
