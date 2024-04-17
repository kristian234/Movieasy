using FluentAssertions;
using Movieasy.Application.Abstractions.Caching;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Reviews.GetReviewSummary;
using Movieasy.Domain.Reviews;
using NSubstitute;

namespace Movieasy.Application.IntegrationTests.Reviews
{
    public class GetReviewSummaryTests : BaseIntegrationTest
    {
        private readonly GetReviewSummaryQuery Query;
        private readonly GetReviewSummaryQueryHandler _handler;

        private readonly ICacheService _cacheServiceMock;

        public GetReviewSummaryTests()
        {
            _cacheServiceMock = Substitute.For<ICacheService>();

            Query = new GetReviewSummaryQuery(Guid.NewGuid());

            _handler = new GetReviewSummaryQueryHandler(_applicationDbContextMock, _cacheServiceMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenReviewSummaryNotFound()
        {
            // Arrange
            _cacheServiceMock
                .GetAsync<ReviewSummaryResponse>(Arg.Any<string>())
                .Returns((ReviewSummaryResponse?)null);

            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.Error.Should().Be(ReviewErrors.NotFound);
        }
    }
}
