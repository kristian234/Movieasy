using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Reviews
{
    public static class ReviewErrors
    {
        public static readonly Error NotFound = new Error(
            "Review.NotFound",
            "A review with the input identifier couldn't be found");

        public static readonly Error NotEligible = new Error(
            "Review.Failure",
            "The review cannot be made");

        public static readonly Error AlreadyPosted = new Error(
            "Review.AlreadyPosted",
            "A review for this movie has already been posted");
    }
}
