namespace Movieasy.Api.Controllers.Reviews
{
    public sealed record AddReviewRequest(
        Guid MovieId,
        string Comment,
        int Rating);
}
