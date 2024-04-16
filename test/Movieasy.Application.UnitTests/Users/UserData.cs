using Movieasy.Domain.Users;

namespace Movieasy.Application.UnitTests.Users
{
    internal static class UserData
    {
        public static readonly string Email = "test@gmail.com";
        public static readonly string FirstName = "Test";
        public static readonly string LastName = "Test";
        public static readonly string Password = "testPassword";

        public static readonly User User = User.Create(
            new FirstName(FirstName),
            new LastName(LastName),
            new Email(Email));
    }
}
