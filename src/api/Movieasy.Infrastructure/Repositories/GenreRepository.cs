using Movieasy.Domain.Genres;
using Movieasy.Domain.Movies;

namespace Movieasy.Infrastructure.Repositories
{
    internal sealed class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext context) : base(context)
        {

        }

        public void Remove(Genre genre)
        {
            DbContext.Remove(genre);
        }
    }
}
