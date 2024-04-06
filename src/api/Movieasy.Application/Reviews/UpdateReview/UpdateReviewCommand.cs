using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Reviews.UpdateReview
{
    public record UpdateReviewCommand(
        Guid ReviewId,
        string Comment,
        int Rating) : ICommand;

}
