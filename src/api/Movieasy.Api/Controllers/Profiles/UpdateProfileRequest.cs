namespace Movieasy.Api.Controllers.Profiles
{
    public sealed record UpdateProfileRequest(
        Guid UserId,
        string Details);
}
