using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Abstractions.Photos;
using Movieasy.Application.Movies.DeleteMovie;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;

namespace Movieasy.Application.Movies.DeleteMovie
{
    internal class DeleteMovieCommandHandler : ICommandHandler<DeleteMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoRepository _photoRepository;
        private readonly IPhotoAccessor _photoAccessor;

        public DeleteMovieCommandHandler(
            IMovieRepository movieRepository,
            IUnitOfWork unitOfWork,
            IPhotoRepository photoRepository,
            IPhotoAccessor photoAccessor)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
            _photoRepository = photoRepository;
            _photoAccessor = photoAccessor;
        }

        public async Task<Result> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            Movie? movie = await _movieRepository.GetByIdAsync(request.MovieId);

            if (movie == null)
            {
                return Result.Failure(MovieErrors.NotFound);
            }

            bool result = await _photoAccessor.DeletePhotoAsync(movie.Photo.PublicId.Value);
            if (!result)
            {
                return Result.Failure(MovieErrors.DeleteFailed);
            }

            _photoRepository.Remove(movie.Photo);

            _movieRepository.Remove(movie);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
