using FluentAssertions;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Reviews.GetReviewById;
using Movieasy.Domain.Reviews;

namespace Movieasy.Application.IntegrationTests.Reviews
{
    public class GetReviewByIdTests : BaseIntegrationTest
    {
        private readonly GetReviewByIdQuery Query;
        private readonly GetReviewByIdQueryHandler _handler;

        public GetReviewByIdTests()
        {
            Query = new GetReviewByIdQuery(Guid.NewGuid());

            _handler = new GetReviewByIdQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenReviewNotFound()
        {
            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.Error.Should().Be(ReviewErrors.NotFound);
        }
    }
}
