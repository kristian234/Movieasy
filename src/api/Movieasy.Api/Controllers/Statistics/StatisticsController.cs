using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Statistics.GetMovieStatistic;
using Movieasy.Domain.Abstractions;
using Movieasy.Application.Statistics;
using Movieasy.Application.Statistics.GetReviewStatistic;

namespace Movieasy.Api.Controllers.Statistics
{
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly ISender _sender;
        public StatisticsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("movies")]
        public async Task<IActionResult> GetMovieStatistics(CancellationToken cancellationToken)
        {
            var query = new GetMovieStatisticsQuery();

            Result<StatisticResponse> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpGet("reviews")]
        public async Task<IActionResult> GetReviewStatistics(CancellationToken cancellationToken)
        {
            var query = new GetReviewStatisticsQuery();

            Result<StatisticResponse> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }
    }
}
