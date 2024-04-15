using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Caching;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Application.Statistics.GetReviewStatistic
{
    internal sealed class GetReviewStatisticsQueryHandler : IQueryHandler<GetReviewStatisticsQuery, StatisticResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public GetReviewStatisticsQueryHandler(
            IApplicationDbContext context,
            ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<Result<StatisticResponse>> Handle(GetReviewStatisticsQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"statistics:review";

            var cachedStatistic = await _cacheService.GetAsync<StatisticResponse>(cacheKey);

            if(cachedStatistic is not null)
            {
                return cachedStatistic;
            }

            StatisticResponse reviewStatistics = new()
            {
                Total = await _context.Reviews.CountAsync()
            };

            await _cacheService.SetAsync(cacheKey, reviewStatistics);

            return reviewStatistics;
        }
    }
}
