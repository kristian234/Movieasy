using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Movies.GetMovie;
using Movieasy.Application.Reviews.AddReview;
using Movieasy.Application.Reviews.GetReview;
using Movieasy.Application.Reviews.GetReviewById;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetReviewByIdQuery(id);

            Result<ReviewResponse> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
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

            return CreatedAtAction(nameof(GetReview), new { id = result.Value }, result.Value);
        }
    }
}
