using FluentAssertions;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Profiles.UpdateProfile;
using Movieasy.Application.UnitTests.Users;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Users;
using NSubstitute;
using System.Runtime.InteropServices;

namespace Movieasy.Application.UnitTests.Profiles
{
    public class UpdateProfileTests
    {
        private readonly UpdateProfileCommand Command;
        private readonly UpdateProfileCommandHandler _handler;

        private readonly IUserRepository _userRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IUserContext _userContextMock;

        public UpdateProfileTests()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _userContextMock = Substitute.For<IUserContext>();

            Command = new UpdateProfileCommand(
                Guid.NewGuid(),
                ProfileData.Details);

            _handler = new UpdateProfileCommandHandler(_userRepositoryMock, _unitOfWorkMock, _userContextMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserNotFound()
        {
            // Arrange
            _userRepositoryMock
                .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
                .Returns((User?)null);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(UserErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUsersDontMatch()
        {
            // Arrange
            _userRepositoryMock
                .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
                .Returns(UserData.User);

            _userContextMock
                .UserId
                .Returns(Guid.NewGuid());

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }
    }
}
