using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Reviews
{
    public sealed record Rating
    {
        public static readonly Error Invalid = new Error("Rating.Invalid", "The rating range is invalid");
        private Rating(int value) => Value = value;
        public int Value { get; init; }

        public static Result<Rating> Create(int value)
        {
            if (value < 1 || value > 5)
            {
                return Result.Failure<Rating>(Invalid);
            }

            return new Rating(value);
        }
    }
}
