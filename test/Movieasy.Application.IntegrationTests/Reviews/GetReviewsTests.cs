using FluentAssertions;
using Movieasy.Application.Common;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Movies.GetMovieById;
using Movieasy.Application.Reviews.GetReview;

namespace Movieasy.Application.IntegrationTests.Reviews
{
    public class GetReviewsTests : BaseIntegrationTest
    {
        private readonly GetReviewsQuery Query;
        private readonly GetReviewsQueryHandler _handler;

        public GetReviewsTests()
        {
            Query = new GetReviewsQuery(Guid.NewGuid(), GlobalData.PageNumber, GlobalData.PageSize, 2, GlobalData.SearchTerm);

            _handler = new GetReviewsQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_ShouldReturn_ValidPagedList()
        {
            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeOfType<PagedList<ReviewResponse>>();
        }
    }
}
