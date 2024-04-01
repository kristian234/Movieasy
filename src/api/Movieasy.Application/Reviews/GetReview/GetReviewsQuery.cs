using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Movies.GetMovie;

namespace Movieasy.Application.Reviews.GetReview
{
    public sealed record GetReviewsQuery(
        Guid MovieId,
        int Page,
        int PageSize,
        int? Rating) : IQuery<PagedList<ReviewResponse>>;
}
