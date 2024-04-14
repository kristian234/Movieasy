using FluentAssertions;
using Movieasy.Application.Genres.DeleteGenre;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Genres;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Genres
{
    public class DeleteGenreTests
    {
        private readonly DeleteGenreCommand Command;
        private readonly DeleteGenreCommandHandler _handler;

        private readonly IGenreRepository _genreRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public DeleteGenreTests()
        {
            _genreRepositoryMock = Substitute.For<IGenreRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            Command = new DeleteGenreCommand(
                Guid.NewGuid());

            _handler = new DeleteGenreCommandHandler(
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
