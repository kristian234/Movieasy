using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Reviews.AddReview
{
    public sealed record AddReviewCommand(
        Guid MovieId,
        string Comment,
        int Rating) : ICommand<Guid>;
}
