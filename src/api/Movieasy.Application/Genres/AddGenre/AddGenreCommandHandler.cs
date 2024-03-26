using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Genres;

namespace Movieasy.Application.Genres.AddGenre
{
    internal sealed class AddGenreCommandHandler : ICommandHandler<AddGenreCommand, Guid>
    {
        private IGenreRepository _genreRepository;
        private IUnitOfWork _unitOfWork;
        public AddGenreCommandHandler(
            IGenreRepository genreRepository,
            IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            Name genreName = new Name(request.Name);

            // TO DO: the name is already indexed, so there shouldn't be any duplicates, but it's a good idea
            // explicitly check it, return the appropriate error so that it's more user friendly.

            Genre genre = Genre.Create(genreName);

            await _genreRepository.AddAsync(genre);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return genre.Id;
        }
    }
}
