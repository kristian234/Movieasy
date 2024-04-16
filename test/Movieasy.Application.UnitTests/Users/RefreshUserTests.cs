using FluentAssertions;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Users.LoginUser;
using Movieasy.Application.Users.RefreshUser;
using Movieasy.Domain.Abstractions;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Users
{
    public class RefreshUserTests
    {
        private readonly RefreshUserCommand Command;
        private readonly RefreshUserCommandHandler _handler;

        private readonly IJwtService _jwtServiceMock;
        public RefreshUserTests()
        {
            _jwtServiceMock = Substitute.For<IJwtService>();    

            Command = new RefreshUserCommand(UserData.RefreshToken);

            _handler = new RefreshUserCommandHandler(_jwtServiceMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnRefreshTokenResponse_WhenTokenIsRefreshedSuccessfully()
        {
            // Arrange
            _jwtServiceMock
                .RefreshTokenAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new JwtServiceResult(UserData.AccessToken, UserData.RefreshToken));

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Value.Should().BeOfType<RefreshTokenResponse>();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenRefreshingTokenFails()
        {
            // Arrange
            _jwtServiceMock
                .RefreshTokenAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(Result.Failure<JwtServiceResult>(UserData.AuthenticationFailedError));

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
        }
    }
}
