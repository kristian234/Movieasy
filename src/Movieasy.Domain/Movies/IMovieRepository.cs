namespace Movieasy.Domain.Movies
{
    public interface IMovieRepository
    {
        public Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task AddAsync(Movie movie);
    }
}
