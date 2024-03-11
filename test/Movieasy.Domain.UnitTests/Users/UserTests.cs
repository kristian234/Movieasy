using FluentAssertions;
using Movieasy.Domain.UnitTests.Infrastructure;
using Movieasy.Domain.Users;
using Movieasy.Domain.Users.Events;

namespace Movieasy.Domain.UnitTests.Users
{
    public class UserTests : BaseTest
    {
        [Fact]
        public void Create_Should_SetValues()
        {
            // Act
            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

            // Assert
            user.FirstName.Should().Be(UserData.FirstName);
            user.LastName.Should().Be(UserData.LastName);
            user.Email.Should().Be(UserData.Email);
        }

        [Fact]
        public void Create_Should_RaiseUserCreatedDomainEvent()
        {
            // Act
            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

            // Assert
            var domainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

            domainEvent.UserId.Should().Be(user.Id);
        }

        [Fact]
        public void Create_Should_AddRegisteredRoleToUser()
        {
            // Act
            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

            // Assert
            user.Roles.Should().Contain(Role.Registered);
        }
    }
}
