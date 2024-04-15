using Movieasy.Application.Abstractions.Messaging;

namespace Movieasy.Application.Statistics.GetMovieStatistic
{
    public sealed record GetMovieStatisticsQuery() : IQuery<StatisticResponse>;
}
