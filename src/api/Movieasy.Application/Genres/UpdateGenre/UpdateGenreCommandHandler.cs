using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Genres;

namespace Movieasy.Application.Genres.UpdateGenre
{
    internal sealed class UpdateGenreCommandHandler : ICommandHandler<UpdateGenreCommand>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGenreCommandHandler(
            IGenreRepository genreRepository,
            IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            Genre? genre = await _genreRepository.GetByIdAsync(request.GenreId);

            if(genre == null)
            {
                return Result.Failure(GenreErrors.NotFound);
            }

            Result updateResult = genre.Update(
                request.Name);

            if (updateResult.IsFailure)
            {
                return updateResult;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
