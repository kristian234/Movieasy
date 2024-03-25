using Microsoft.EntityFrameworkCore;
using Movieasy.Domain.Movies;

namespace Movieasy.Infrastructure.Repositories
{
    internal sealed class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .Include(x => x.Photo) // Aware that this may decrease performance, but it's a worthy tradeoff given the current situation where a movie CANNOT exist without a photo
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Remove(Movie movie)
        {
            _context.Movies.Remove(movie);
        }
    }
}
