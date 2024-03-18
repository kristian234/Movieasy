using Microsoft.AspNetCore.Http;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Application.Abstractions.Photos
{
    public interface IPhotoAccessor
    {
        Task<Result<PhotoUploadResult>> AddPhoto(IFormFile file);

        Task<bool> DeletePhoto(string publicId);
    }
}
