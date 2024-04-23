using Movieasy.Application.Reviews.GetReview;

namespace Movieasy.Application.Profiles.GetProfileById
{
    public sealed class ProfileResponse
    {
        public string Id { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Details { get; init; } = string.Empty;
    }
}
