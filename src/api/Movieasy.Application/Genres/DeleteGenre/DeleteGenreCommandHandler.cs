using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Genres;

namespace Movieasy.Application.Genres.DeleteGenre
{
    internal class DeleteGenreCommandHandler : ICommandHandler<DeleteGenreCommand>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGenreCommandHandler(
            IGenreRepository genreRepository,
            IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            Genre? genre = await _genreRepository.GetByIdAsync(request.GenreId);

            if (genre == null)
            {
                return Result.Failure(GenreErrors.NotFound);
            }

            _genreRepository.Remove(genre);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
