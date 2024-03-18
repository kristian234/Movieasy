using Movieasy.Application.Abstractions.Clock;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Abstractions.Photos;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;

namespace Movieasy.Application.Movies.AddMovie
{
    internal sealed class AddMovieCommandHandler : ICommandHandler<AddMovieCommand, Guid>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IPhotoRepository _photoRepository;

        public AddMovieCommandHandler(
            IMovieRepository movieRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            IPhotoAccessor photoAccessor,
            IPhotoRepository photoRepository)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _photoAccessor = photoAccessor;
            _photoRepository = photoRepository;
        }

        public async Task<Result<Guid>> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            Result<Duration> movieDuration = Duration.Create(request.Duration);

            if (movieDuration.IsFailure)
            {
                return Result.Failure<Guid>(Duration.Invalid);
            }

            Result<PhotoUploadResult> result = await _photoAccessor.AddPhoto(request.Photo);

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            Photo moviePhoto = Photo.Create(
                new PublicId(result.Value.PublicId),
                new Url(result.Value.Url));

            await _photoRepository.AddAsync(moviePhoto);

            Title movieTitle = new Title(request.Title);
            Description movieDescription = new Description(request.Description);

            Rating movieRating = (Rating)request.Rating;

            Movie movie = Movie.Create(
                movieTitle,
                movieDescription,
                movieRating,
                movieDuration.Value,
                _dateTimeProvider.UtcNow,
                moviePhoto,
                request.ReleaseDate);

            await _movieRepository.AddAsync(movie);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }
    }
}
