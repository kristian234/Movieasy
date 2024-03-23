using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Abstractions.Photos;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;
using System.Runtime.InteropServices;

namespace Movieasy.Application.Movies.UpdateMovie
{
    internal class UpdateMovieCommandHandler : ICommandHandler<UpdateMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoAccessor _photoAccessor;

        public UpdateMovieCommandHandler(
            IMovieRepository movieRepository,
            IUnitOfWork unitOfWork,
            IPhotoAccessor photoAccessor,
            IPhotoRepository photoRepository)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
            _photoAccessor = photoAccessor;
        }

        public async Task<Result> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            Movie? movie = await _movieRepository.GetByIdAsync(request.MovieId);

            if (movie == null)
            {
                return Result.Failure(MovieErrors.NotFound);
            }

            Result updateResult = movie.Update(
                request.Title,
                request.Description,
                request.Rating,
                request.Duration,
                request.ReleaseDate);

            if (updateResult.IsFailure)
            {
                return updateResult;
            }

            if (request.Photo == null)
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }

            // Add photo to cloudinary
            Result<PhotoUploadResult> result = await _photoAccessor.AddPhoto(request.Photo);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            Photo oldPhoto = movie.Photo;

            // Remove old photo from cloudinary
            bool deleteResult = await _photoAccessor.DeletePhoto(oldPhoto.PublicId.Value);
            if (!deleteResult)
            {
                return Result.Failure(MovieErrors.UpdateFailed);
            }

            // Change old photo to new
            movie.Photo.Update(
                result.Value.PublicId,
                result.Value.Url);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
