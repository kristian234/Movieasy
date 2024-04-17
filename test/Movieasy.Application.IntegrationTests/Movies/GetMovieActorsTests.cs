using FluentAssertions;
using Movieasy.Application.Actors.GetMovieActorsQueryHandler;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Movies.GetMovieActorsQuery;
using Movieasy.Domain.Movies;

namespace Movieasy.Application.IntegrationTests.Movies
{
    public class GetMovieActorsTests : BaseIntegrationTest
    {
        private readonly GetMovieActorsQuery Query;
        private readonly GetMovieActorsQueryHandler _handler;

        public GetMovieActorsTests()
        {
            Query = new GetMovieActorsQuery(Guid.NewGuid());

            _handler = new GetMovieActorsQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenMovieNotFound()
        {
            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.Error.Should().Be(MovieErrors.NotFound);
        }
    }
}
