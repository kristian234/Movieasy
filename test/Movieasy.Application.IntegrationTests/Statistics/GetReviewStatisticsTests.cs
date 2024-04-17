using FluentAssertions;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Movieasy.Application.Abstractions.Caching;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Statistics.GetReviewStatistic;
using NSubstitute;
using StackExchange.Redis;

namespace Movieasy.Application.IntegrationTests.Statistics
{
    public class GetReviewStatisticsTests : BaseIntegrationTest
    {
        private readonly GetReviewStatisticsQuery Query;
        private readonly GetReviewStatisticsQueryHandler _handler;

        private readonly ICacheService _cacheServiceMock;

        public GetReviewStatisticsTests()
        {
            _cacheServiceMock = Substitute.For<ICacheService>();

            Query = new GetReviewStatisticsQuery();

            _handler = new GetReviewStatisticsQueryHandler(_applicationDbContextMock, _cacheServiceMock);
        }

        [Fact]
        public async Task Handle_ShouldReturn_ReviewStatisticSuccessfully()
        {
            // Act  
            var result = await _handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
