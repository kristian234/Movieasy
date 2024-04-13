using Movieasy.Domain.Actors;
using Movieasy.Domain.Genres;

namespace Movieasy.Domain.Reviews
{
    public interface IReviewRepository
    {
        public Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<Review?> GetByUserAndMovieIdAsync(Guid userId, Guid movieId, CancellationToken cancellationToken = default);
        public Task AddAsync(Review review);
        public void Remove(Review genre);
    }
}
