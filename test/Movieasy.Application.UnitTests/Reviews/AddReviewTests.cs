using FluentAssertions;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Abstractions.Clock;
using Movieasy.Application.Reviews.AddReview;
using Movieasy.Application.UnitTests.Actors;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Reviews;
using Movieasy.Domain.Users;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Reviews
{
    public class AddReviewTests
    {
        private readonly AddReviewCommand Command;
        private readonly AddReviewCommandHandler _handler;

        private readonly IReviewRepository _reviewRepositoryMock;
        private readonly IMovieRepository _movieRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;

        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IUserContext _userContextMock;
        private readonly IDateTimeProvider _dateTimeProviderMock;
        public AddReviewTests()
        {
            _reviewRepositoryMock = Substitute.For<IReviewRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _movieRepositoryMock = Substitute.For<IMovieRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _userContextMock = Substitute.For<IUserContext>();
            _dateTimeProviderMock = Substitute.For<IDateTimeProvider>();

            Command = new AddReviewCommand(
                Guid.NewGuid(),
                ReviewData.Comment,
                ReviewData.Rating);

            _handler = new AddReviewCommandHandler(
                _reviewRepositoryMock,
                _movieRepositoryMock,
                _userRepositoryMock,
                _userContextMock,
                _dateTimeProviderMock,
                _unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenMovieNotFound()
        {
            // Arrange
            _movieRepositoryMock
                .GetByIdAsync(Command.MovieId, Arg.Any<CancellationToken>())
                .Returns((Movie?)null);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(ReviewErrors.NotEligible);
        } 
        
        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserNotFound()
        {
            // Arrange
            _movieRepositoryMock
                .GetByIdAsync(Command.MovieId, Arg.Any<CancellationToken>())
                .Returns(ActorData.Movie);

            _userRepositoryMock
                .GetByIdAsync(new Guid(), Arg.Any<CancellationToken>())
                .Returns((User?)null);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(ReviewErrors.NotEligible);
        }
    }
}
