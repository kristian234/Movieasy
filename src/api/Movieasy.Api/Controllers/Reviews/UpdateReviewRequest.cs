namespace Movieasy.Api.Controllers.Reviews
{
    public sealed record UpdateReviewRequest(
        Guid ReviewId,
        string Comment,
        int Rating);
}
