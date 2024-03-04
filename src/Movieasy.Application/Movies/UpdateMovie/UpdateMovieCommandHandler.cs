using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;

namespace Movieasy.Application.Movies.UpdateMovie
{
    internal class UpdateMovieCommandHandler : ICommandHandler<UpdateMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMovieCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
