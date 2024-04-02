using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Reviews.GetReviewSummary
{
    public sealed record GetReviewSummaryQuery(
        Guid MovieId) : IQuery<ReviewSummaryResponse>;
}
