using Microsoft.AspNetCore.Http;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Application.Abstractions.Photos
{
    public interface IPhotoAccessor
    {
        Task<Result<PhotoUploadResult>> AddPhotoAsync(IFormFile file);

        Task<bool> DeletePhotoAsync(string publicId);
    }
}
