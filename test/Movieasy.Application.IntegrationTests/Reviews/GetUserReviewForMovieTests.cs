using FluentAssertions;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Abstractions.Caching;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Reviews.GetReviewSummary;
using Movieasy.Application.Reviews.GetUserReviewForMovie;
using Movieasy.Domain.Reviews;
using NSubstitute;

namespace Movieasy.Application.IntegrationTests.Reviews
{
    public class GetUserReviewForMovieTests : BaseIntegrationTest
    {
        private readonly GetUserReviewForMovieQuery Query;
        private readonly GetUserReviewForMovieQueryHandler _handler;

        private readonly IUserContext _userContextMock;
        public GetUserReviewForMovieTests()
        {
            _userContextMock = Substitute.For<IUserContext>();

            Query = new GetUserReviewForMovieQuery(Guid.NewGuid());

            _handler = new GetUserReviewForMovieQueryHandler(_applicationDbContextMock, _userContextMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenReviewSummaryNotFound()
        {
            // Arrange
            _userContextMock
                .UserId
                .Returns(Guid.NewGuid());

            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.Error.Should().Be(ReviewErrors.NotFound);
        }
    }
}
