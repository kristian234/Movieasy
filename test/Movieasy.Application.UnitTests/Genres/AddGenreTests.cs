using Movieasy.Application.Genres.AddGenre;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Genres;
using NSubstitute;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Movieasy.Application.UnitTests.Genres
{
    public class AddGenreTests
    {
        private readonly AddGenreCommand Command;
        private readonly AddGenreCommandHandler _handler;

        private readonly IGenreRepository _genreRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public AddGenreTests()
        {
            _genreRepositoryMock = Substitute.For<IGenreRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            Command = new AddGenreCommand(
                GenreData.Name);

            _handler = new AddGenreCommandHandler(
                _genreRepositoryMock,
                _unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnNonEmptyGuid_WhenGenreAddedSuccessfully()
        {
            // Arrange
            _genreRepositoryMock.AddAsync(Arg.Any<Genre>()).ReturnsForAnyArgs(Task.CompletedTask);
            _unitOfWorkMock.SaveChangesAsync().ReturnsForAnyArgs(1);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            Assert.NotEqual(Guid.Empty, result.Value);
            Assert.IsType<Guid>(result.Value);
        }
    }
}
