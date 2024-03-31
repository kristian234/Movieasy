using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Api.Controllers.Genres;
using Movieasy.Application.Movies.GetMovie;
using Movieasy.Application.Reviews.AddReview;
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
            AddReviewRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddReviewCommand(
                request.MovieId,
                request.Comment,
                request.Rating);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(); // TO DO: fix this so it returns an endpoint to only get the review itself and nothing more.
        }
    }
}
