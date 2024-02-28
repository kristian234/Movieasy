using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Movies.AddMovie;

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
        public async Task<IActionResult> GetMovie(Guid id, CancellationToken cancellationToken)
        {
            return Ok();
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

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return CreatedAtAction(nameof(GetMovie), result.Value);
        }
    }
}
