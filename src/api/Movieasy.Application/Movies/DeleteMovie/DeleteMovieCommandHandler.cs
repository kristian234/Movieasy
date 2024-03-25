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

        public DeleteMovieCommandHandler(
            IMovieRepository movieRepository,
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            Movie? movie = await _movieRepository.GetByIdAsync(request.MovieId);

            if (movie == null)
            {
                return Result.Failure(MovieErrors.NotFound);
            }

            _movieRepository.Remove(movie);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
