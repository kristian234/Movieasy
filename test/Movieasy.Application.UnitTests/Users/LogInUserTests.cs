using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Users.LoginUser;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Users
{
    public class LogInUserTests
    {
        private readonly LogInUserCommand Command;
        private readonly LogInUserCommandHandler _handler;

        private readonly IJwtService _jwtServiceMock;

        public LogInUserTests()
        {
            _jwtServiceMock = Substitute.For<IJwtService>();

            Command = new LogInUserCommand(UserData.Email, UserData.Password, true);

            _handler = new LogInUserCommandHandler(_jwtServiceMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnAccessTokenResponse_WhenUserLogsInSuccessfully()
        {
            // Arrange
            const string accessToken = "test access token";
            const string refreshToken = "test refresh token";

            _jwtServiceMock
                .GetAccessTokenAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new JwtServiceResult(accessToken, refreshToken));

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Value.AccessToken.Should().BeSameAs(accessToken);
            result.Value.RefreshToken.Should().BeSameAs(refreshToken);
        }

        [Fact]
        public async Task Handle_ShouldNot_ReturnRefreshToken_WhenRememberMeFalse()
        {
            // Arrange
            LogInUserCommand newCommand = new LogInUserCommand(UserData.Email, UserData.Password, false);

            const string accessToken = "test access token";
            const string refreshToken = "test refresh token";

            _jwtServiceMock
                .GetAccessTokenAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new JwtServiceResult(accessToken, refreshToken));

            // Act
            var result = await _handler.Handle(newCommand, default);

            // Assert
            accessToken.Should().BeSameAs(result.Value.AccessToken);
            result.Value.RefreshToken.Should().BeNull();
        }
    }
}
