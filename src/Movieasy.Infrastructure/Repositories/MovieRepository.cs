using Movieasy.Domain.Movies;

namespace Movieasy.Infrastructure.Repositories
{
    internal sealed class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
