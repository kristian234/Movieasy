using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Reviews
{
    public static class ReviewErrors
    {
        public static readonly Error NotEligible = new Error(
            "Review.NotEligible",
            "The review is not eligible because the booking is not yet completed");
    }
}
