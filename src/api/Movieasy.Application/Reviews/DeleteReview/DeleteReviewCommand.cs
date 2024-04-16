
using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Reviews.DeleteReview
{
    public sealed record DeleteReviewCommand(Guid ReviewId) : ICommand;
}
