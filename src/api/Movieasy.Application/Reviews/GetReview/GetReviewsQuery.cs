using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Common;

namespace Movieasy.Application.Reviews.GetReview
{
    public sealed record GetReviewsQuery(
        Guid MovieId,
        int Page,
        int PageSize,
        int? Rating,
        string? SortTerm) : IQuery<PagedList<ReviewResponse>>;
}
