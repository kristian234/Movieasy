using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Reviews.GetReview;

namespace Movieasy.Application.Reviews.GetUserReviewForMovie
{
    public sealed record GetUserReviewForMovieQuery(
        Guid MovieId) : IQuery<ReviewResponse>;
}
