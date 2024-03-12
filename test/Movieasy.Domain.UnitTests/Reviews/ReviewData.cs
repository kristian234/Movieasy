using Movieasy.Domain.Movies;
using Movieasy.Domain.Users;

namespace Movieasy.Domain.UnitTests.Reviews
{
    internal static class ReviewData
    {
        public static readonly User user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);
    }
}
