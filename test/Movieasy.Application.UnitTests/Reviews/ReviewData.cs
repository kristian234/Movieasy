using Movieasy.Domain.Reviews;
using Movieasy.Domain.Users;

namespace Movieasy.Application.UnitTests.Reviews
{
    internal static class ReviewData
    {
        public static readonly string Comment = "Test comment";
        public static readonly int Rating = 4;

        public static readonly Review Review = Review.Create(
            MovieData.Movie,
            User.Create(
                new FirstName("Test name"),
                new LastName("Test last name"),
                new Email("bjorn@gmail.com")),
            Domain.Reviews.Rating.Create(Rating).Value,
            new Comment(Comment),
            DateTime.UtcNow).Value;
    }
}
