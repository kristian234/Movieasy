using System.Security.Cryptography.X509Certificates;

namespace Movieasy.Domain.Reviews
{
    public sealed record Rating
    {
        private Rating(int value) => Value = value;
        public int Value { get; init; }

        public static Rating Create(int value)
        {
            if (value < 1 || value > 5)
            {
                throw new ApplicationException("Rating exceeds limits");
            }

            return new Rating(value);
        }
    }
}
