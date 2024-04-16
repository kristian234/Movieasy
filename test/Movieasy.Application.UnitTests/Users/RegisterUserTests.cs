using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Users.RegisterUser;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Users;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Users
{
    public class RegisterUserTests
    {
        private readonly RegisterUserCommand Command;
        private readonly RegisterUserCommandHandler _handler;

        private readonly IUserRepository _userRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IAuthenticationService _authenticationServiceMock;

        public RegisterUserTests()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _authenticationServiceMock = Substitute.For<IAuthenticationService>();

            Command = new RegisterUserCommand(UserData.Email, UserData.FirstName, UserData.LastName, UserData.Password);

            _handler = new RegisterUserCommandHandler(
                _userRepositoryMock,
                _unitOfWorkMock,
                _authenticationServiceMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnNonEmptyGuid_WhenUserRegisteredSuccessfully()
        {
            // Arrange
            _authenticationServiceMock
                .RegisterAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns("testId");

            _userRepositoryMock.AddAsync(Arg.Any<User>()).Returns(Task.CompletedTask);
            _unitOfWorkMock.SaveChangesAsync().Returns(1);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            Assert.NotEqual(Guid.Empty, result.Value);
            Assert.IsType<Guid>(result.Value);
        }
    }
}
