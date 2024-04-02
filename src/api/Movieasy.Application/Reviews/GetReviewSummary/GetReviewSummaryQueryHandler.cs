using Microsoft.EntityFrameworkCore;
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

        public GetReviewSummaryQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ReviewSummaryResponse>> Handle(GetReviewSummaryQuery request, CancellationToken cancellationToken)
        {
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
