using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Reviews.GetReview;

namespace Movieasy.Application.Reviews.GetReviewById
{
    public sealed record GetReviewByIdQuery(
        Guid ReviewId) : IQuery<ReviewResponse>;
}
