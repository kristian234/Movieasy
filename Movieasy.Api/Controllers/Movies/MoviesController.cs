using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Movies.AddMovie;
using Movieasy.Application.Movies.GetMovie;
using Movieasy.Application.Movies.GetMovieById;
using Movieasy.Application.Movies.UpdateMovie;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Api.Controllers.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ISender _sender;
        public MoviesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = new GetMoviesQuery(searchTerm, sortColumn, sortOrder, page, pageSize);

            Result<PagedList<MovieResponse>> result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetMovieByIdQuery(id);

            Result<MovieResponse> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(
            AddMovieRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddMovieCommand(
                request.Title,
                request.Description,
                request.Rating,
                request.ReleaseDate,
                request.Duration);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetMovie), new { id = result.Value }, result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie(
            UpdateMovieRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateMovieCommand(
                request.MovieId,
                request.Title,
                request.Description,
                request.Rating,
                request.ReleaseDate,
                request.Duration);

            Result result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
