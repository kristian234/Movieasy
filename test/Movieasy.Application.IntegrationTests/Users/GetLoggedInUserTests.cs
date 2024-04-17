using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Users.GetLoggedInUser;
using Movieasy.Domain.Users;
using NSubstitute;

namespace Movieasy.Application.IntegrationTests.Users
{
    public class GetLoggedInUserTests : BaseIntegrationTest
    {
        private readonly GetLoggedInUserQuery Query;
        private readonly GetLoggedInUserQueryHandler _handler;

        private readonly IUserContext _userContextMock;

        public GetLoggedInUserTests()
        {
            _userContextMock = Substitute.For<IUserContext>();

            Query = new GetLoggedInUserQuery();

            _handler = new GetLoggedInUserQueryHandler(_applicationDbContextMock, _userContextMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserNotExist()
        {
            // Arrange
            _userContextMock.IdentityId
                .Returns("test");

            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }
    }
}
