using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Movies.GetMovieById;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Movies
{
    public class GetMovieByIdTests
    {
        private readonly GetMovieByIdQuery Query;
        private readonly GetMovieByIdQueryHandler _handler;

        private readonly IApplicationDbContext _applicationDbContextMock;

        public GetMovieByIdTests()
        {
            _applicationDbContextMock = Substitute.For<IApplicationDbContext>();

            Query = new GetMovieByIdQuery(Guid.NewGuid());

            _handler = new GetMovieByIdQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenMovieNotFound()
        {
            // Arrange
            //_applicationDbContextMock.Movies.

            // Act

            // Assert
        }
    }
}
