using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Api.Controllers.Genres;
using Movieasy.Application.Movies.GetMovie;
using Movieasy.Application.Reviews.GetReview;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Api.Controllers.Reviews
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ISender _sender;

        public ReviewsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(
            Guid movieId,
            CancellationToken cancellationToken,
            int pageNumber = 1,
            int pageSize = 12)
        {
            var query = new GetReviewsQuery(
                movieId,
                pageNumber,
                pageSize);

            Result<PagedList<ReviewResponse>> result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(
            )
        {

        }
    }
}
