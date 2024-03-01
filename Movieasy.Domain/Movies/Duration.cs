using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Movies
{
    public record Duration
    {
        public static readonly Error Invalid = new Error("Duration.Invalid", "The Duration range is invalid");
        private Duration(double value) => Value = value;
        public double Value { get; set; }

        public static Result<Duration> Create(double value)
        {
            if (value < 0)
            {
                return Result.Failure<Duration>(Invalid);
            }

            return new Duration(value);
        }
    }
}
