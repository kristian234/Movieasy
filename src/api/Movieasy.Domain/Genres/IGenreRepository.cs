using Movieasy.Domain.Movies;

namespace Movieasy.Domain.Genres
{
    public interface IGenreRepository
    {
        public Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task AddAsync(Genre genre);
        public void Remove(Genre genre);
    }
}
