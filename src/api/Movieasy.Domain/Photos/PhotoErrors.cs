using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Photos
{
    public static class PhotoErrors
    {
        public static readonly Error UploadingImageFailed = new(
           "PhotoAccessor.UploadingImageFailed",
           "Failed to upload the image");
    }
}
