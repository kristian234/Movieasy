using Movieasy.Application.Genres.AddGenre;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Genres;
using NSubstitute;

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
        
        
        // No edge cases to really test at the moment..
    }
}
