using FluentAssertions;
using Movieasy.Application.Abstractions.Caching;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Statistics.GetMovieStatistic;
using NSubstitute;

namespace Movieasy.Application.IntegrationTests.Statistics
{
    public class GetMovieStatisticsTests : BaseIntegrationTest
    {
        private readonly GetMovieStatisticsQuery Query;
        private readonly GetMovieStatisticsQueryHandler _handler;

        private readonly ICacheService _cacheServiceMock;

        public GetMovieStatisticsTests()
        {
            _cacheServiceMock = Substitute.For<ICacheService>();    

            Query = new GetMovieStatisticsQuery();

            _handler = new GetMovieStatisticsQueryHandler(_applicationDbContextMock, _cacheServiceMock);
        }

        [Fact]
        public async Task Handle_ShouldReturn_MovieStatisticSuccessfully()
        {
            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
