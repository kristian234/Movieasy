using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Statistics.GetReviewStatistic
{
    public sealed record GetReviewStatisticsQuery() : IQuery<StatisticResponse>;
}
