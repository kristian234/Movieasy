namespace Movieasy.Domain.Reviews
{
    public interface IReviewRepository
    {
        public Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task AddAsync(Review review);
    }
}
