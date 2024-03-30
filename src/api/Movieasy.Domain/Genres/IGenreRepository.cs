using Movieasy.Domain.Movies;
using System.Runtime.CompilerServices;

namespace Movieasy.Domain.Genres
{
    public interface IGenreRepository
    {
        public Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Genre>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);  
        public Task AddAsync(Genre genre);
        public void Remove(Genre genre);
    }
}
