using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Movieasy.Application.Abstractions.Photos;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Infrastructure.Photos
{
    internal sealed class PhotoAccessor : IPhotoAccessor
    {
        private static readonly Domain.Abstractions.Error UploadingImageFailed = new(
           "PhotoAccessor.UploadingImageFailed",
           "Failed to upload the image");

        private readonly Cloudinary _cloudinary;
        private readonly CloudinaryOptions _cloudinaryOptions;

        public PhotoAccessor(IOptions<CloudinaryOptions> cloudinaryOptions)
        {
            _cloudinaryOptions = cloudinaryOptions.Value;

            var account = CreateAccount();

            _cloudinary = new Cloudinary(account);
        }

        private Account CreateAccount()
        {
            return new Account(
                _cloudinaryOptions.CloudName,
                _cloudinaryOptions.ApiKey,
                _cloudinaryOptions.ApiSecret
            );
        }

        public async Task<Result<PhotoUploadResult>> AddPhoto(IFormFile file)
        {
            if (file.Length < 0) return Result.Failure<PhotoUploadResult>(UploadingImageFailed);

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                return Result.Failure<PhotoUploadResult>(UploadingImageFailed);       
            }

            return new PhotoUploadResult(
                uploadResult.PublicId,
                uploadResult.SecureUrl.ToString()); 
        }

        public async Task<bool> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok" ? true : false;    
        }
    }
}
