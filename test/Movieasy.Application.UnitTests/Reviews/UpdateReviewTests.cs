using FluentAssertions;
using Movieasy.Application.Reviews.UpdateReview;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Reviews;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Reviews
{
    public class UpdateReviewTests
    {
        private readonly UpdateReviewCommand Command;
        private readonly UpdateReviewCommandHandler _handler;

        private readonly IReviewRepository _reviewRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public UpdateReviewTests()
        {
            _reviewRepositoryMock = Substitute.For<IReviewRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            Command = new UpdateReviewCommand(
                Guid.NewGuid(),
                ReviewData.Comment,
                ReviewData.Rating);

            _handler = new UpdateReviewCommandHandler(
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

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUpdateResultInvalid()
        {
            // Arrange
            UpdateReviewCommand brokenCommand =
                new UpdateReviewCommand(Guid.NewGuid(), ReviewData.Comment, ReviewData.Rating);

            _reviewRepositoryMock
                .GetByIdAsync(brokenCommand.ReviewId, Arg.Any<CancellationToken>())
                    .Returns(ReviewData.Review);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
        }
    }
}
