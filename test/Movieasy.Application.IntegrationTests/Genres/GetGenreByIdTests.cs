using FluentAssertions;
using Movieasy.Application.Genres.GetGenreById;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Movies.GetMovieById;
using Movieasy.Domain.Genres;
using Movieasy.Domain.Movies;

namespace Movieasy.Application.IntegrationTests.Genres
{
    public class GetGenreByIdTests : BaseIntegrationTest
    {
        private readonly GetGenreByIdQuery Query;
        private readonly GetGenreByIdQueryHandler _handler;

        public GetGenreByIdTests()
        {
            Query = new GetGenreByIdQuery(Guid.NewGuid());

            _handler = new GetGenreByIdQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenMovieNotFound()
        {
            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.Error.Should().Be(GenreErrors.NotFound);
        }
    }
}
