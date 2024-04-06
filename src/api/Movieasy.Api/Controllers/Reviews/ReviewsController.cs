using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Movies.GetMovie;
using Movieasy.Application.Reviews.AddReview;
using Movieasy.Application.Reviews.GetReview;
using Movieasy.Application.Reviews.GetReviewById;
using Movieasy.Application.Reviews.GetReviewSummary;
using Movieasy.Application.Reviews.GetUserReviewForMovie;
using Movieasy.Application.Reviews.UpdateReview;
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

        [HttpGet("user-review/{movieId}")]
        public async Task<IActionResult> GetUserReviewForMovie(
            Guid movieId,
            CancellationToken cancellationToken)
        {
            var query = new GetUserReviewForMovieQuery(
                movieId);

            Result<ReviewResponse> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
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

        [HttpGet("summary/{id}")]
        public async Task<IActionResult> GetReviewSummary(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetReviewSummaryQuery(id);

            Result<ReviewSummaryResponse> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(
            Guid movieId,
            CancellationToken cancellationToken,
            int? rating,
            string? sortTerm,
            int pageNumber = 1,
            int pageSize = 12)
        {
            var query = new GetReviewsQuery(
                movieId,
                pageNumber,
                pageSize,
                rating,
                sortTerm);

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

        [HttpPut]
        public async Task<IActionResult> UpdateReview(
            UpdateReviewRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateReviewCommand(
                request.ReviewId,
                request.Comment,
                request.Rating);

            Result result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
