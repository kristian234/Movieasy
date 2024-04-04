using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Abstractions.Photos;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Genres;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;

namespace Movieasy.Application.Movies.UpdateMovie
{
    internal sealed class UpdateMovieCommandHandler : ICommandHandler<UpdateMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IGenreRepository _genreRepository;

        public UpdateMovieCommandHandler(
            IMovieRepository movieRepository,
            IUnitOfWork unitOfWork,
            IPhotoAccessor photoAccessor,
            IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
            _photoAccessor = photoAccessor;
            _genreRepository = genreRepository;
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
                request.TrailerUrl,
                request.ReleaseDate);

            if (updateResult.IsFailure)
            {
                return updateResult;
            }

            IEnumerable<Genre> genres = await _genreRepository.GetByIdsAsync(request.Genres);
            if (genres.Count() != request.Genres.Count)
            {
                return Result.Failure<Guid>(GenreErrors.NotFound);
            }

            movie.SetGenres(genres);

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
