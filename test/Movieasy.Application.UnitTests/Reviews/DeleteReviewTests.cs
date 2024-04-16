using FluentAssertions;
using Movieasy.Application.Reviews.DeleteReview;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Reviews;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Reviews
{
    public class DeleteReviewTests
    {
        private readonly DeleteReviewCommand Command;
        private readonly DeleteReviewCommandHandler _handler;

        private readonly IReviewRepository _reviewRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public DeleteReviewTests()
        {
            _reviewRepositoryMock = Substitute.For<IReviewRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            Command = new DeleteReviewCommand(
                Guid.NewGuid());

            _handler = new DeleteReviewCommandHandler(
                _reviewRepositoryMock,
                _unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenReviewNotFound()
        {
            // Arrange
            _reviewRepositoryMock
                .GetByIdAsync(Command.ReviewId, Arg.Any<CancellationToken>())
                .Returns((Review?)null);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(ReviewErrors.NotFound);
        }
    }
}
