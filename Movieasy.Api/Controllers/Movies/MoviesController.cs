using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Movies.AddMovie;
using Movieasy.Application.Movies.GetMovie;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetMovieQuery(id);

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
                request.Duration);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return CreatedAtAction(nameof(GetMovie), result.Value);
        }
    }
}
