using Movieasy.Domain.Reviews;

namespace Movieasy.Domain.UnitTests.Reviews
{
    internal static class ReviewData
    {
        public static readonly Comment Comment = new("Test comment for review");
        public static readonly Rating Rating = Rating.Create(3).Value;
        public static readonly DateTime CreatedOnDate = new DateTime(2022, 4, 12, 13, 30, 0);
    }
}
