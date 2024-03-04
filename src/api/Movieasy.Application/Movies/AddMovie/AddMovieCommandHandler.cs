using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;

namespace Movieasy.Application.Movies.AddMovie
{
    internal sealed class AddMovieCommandHandler : ICommandHandler<AddMovieCommand, Guid>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddMovieCommandHandler(
            IMovieRepository movieRepository,
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            Result<Duration> movieDuration = Duration.Create(request.Duration);

            if (movieDuration.IsFailure)
            {
                return Result.Failure<Guid>(Duration.Invalid);
            }

            Title movieTitle = new Title(request.Title);
            Description movieDescription = new Description(request.Description);
            Rating movieRating = (Rating)request.Rating;

            Movie movie = Movie.Create(movieTitle, movieDescription, movieRating, movieDuration.Value, request.ReleaseDate);

            await _movieRepository.AddAsync(movie);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }
    }
}
