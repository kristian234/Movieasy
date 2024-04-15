using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Caching;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Application.Statistics.GetMovieStatistic
{
    internal sealed class GetMovieStatisticsQueryHandler : IQueryHandler<GetMovieStatisticsQuery, StatisticResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;
        public GetMovieStatisticsQueryHandler(
            IApplicationDbContext context,
            ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<Result<StatisticResponse>> Handle(GetMovieStatisticsQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"statistics:movie";

            var cachedStatistic = await _cacheService.GetAsync<StatisticResponse>(cacheKey);

            if (cachedStatistic is not null)
            {
                return cachedStatistic;
            }

            StatisticResponse movieStatistics = new()
            {
                Total = await _context.Movies.CountAsync(cancellationToken),
            };

            await _cacheService.SetAsync(cacheKey, movieStatistics);

            return movieStatistics;
        }
    }
}
