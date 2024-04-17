using FluentAssertions;
using Movieasy.Application.Genres.GetGenre;
using Movieasy.Application.IntegrationTests.Infrastructure;

namespace Movieasy.Application.IntegrationTests.Genres
{
    public class GetGenresTests : BaseIntegrationTest
    {
        private readonly GetGenresQuery Query;
        private readonly GetGenresQueryHandler _handler;

        public GetGenresTests()
        {
            Query = new GetGenresQuery();

            _handler = new GetGenresQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_ShouldReturn_GenresSuccessfully()
        {
            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
