namespace Movieasy.Application.Abstractions.Photos
{
    public sealed record PhotoUploadResult(
        string PublicId,
        string Url);
}
