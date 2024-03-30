using Microsoft.EntityFrameworkCore;
using Movieasy.Domain.Genres;

namespace Movieasy.Infrastructure.Repositories
{
    internal sealed class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Genre>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await DbContext.Genres
                .Where(genre => ids.Contains(genre.Id))
                .ToListAsync();
        }

        public void Remove(Genre genre)
        {
            DbContext.Remove(genre);
        }
    }
}
