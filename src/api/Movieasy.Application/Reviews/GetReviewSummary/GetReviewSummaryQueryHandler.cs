using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Caching;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Reviews;

namespace Movieasy.Application.Reviews.GetReviewSummary
{
    internal sealed class GetReviewSummaryQueryHandler 
        : IQueryHandler<GetReviewSummaryQuery, ReviewSummaryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public GetReviewSummaryQueryHandler(
            IApplicationDbContext context,
            ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<Result<ReviewSummaryResponse>> Handle(GetReviewSummaryQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"reviews:summary-{request.MovieId}";

            var cachedSummary = await _cacheService.GetAsync<ReviewSummaryResponse>(cacheKey);

            if (cachedSummary is not null)
            {
                return cachedSummary;
            }

            ReviewSummaryResponse? reviewSummary = await _context.Reviews
                .Where(r => r.MovieId == request.MovieId)
                .GroupBy(r => r.MovieId)
                .Select(g => new ReviewSummaryResponse()
                {
                    AverageRating = g.Average(r => (int)r.Rating),
                    TotalRatings = g.Count()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if(reviewSummary == null)
            {
                return Result.Failure<ReviewSummaryResponse>(ReviewErrors.NotFound);
            }

            return reviewSummary;
        }
    }
}
