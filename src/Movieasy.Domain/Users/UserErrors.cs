using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound = new Error(
            "User.NotFound",
            "The user with the specified identifier was not found");

        public static Error InvalidCredentials = new Error(
            "User.InvalidCredentials",
            "The provided credentials were invalid");
    }
}
