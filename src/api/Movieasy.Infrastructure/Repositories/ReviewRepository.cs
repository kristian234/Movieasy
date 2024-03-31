using Microsoft.EntityFrameworkCore;
using Movieasy.Domain.Reviews;

namespace Movieasy.Infrastructure.Repositories
{
    internal sealed class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Review?> GetByUserAndMovieIdAsync(Guid userId, Guid movieId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
        }
    }
}
