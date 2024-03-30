using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Genres.AddGenre;
using Movieasy.Application.Genres.DeleteGenre;
using Movieasy.Application.Genres.GetGenre;
using Movieasy.Application.Genres.GetGenreById;
using Movieasy.Application.Genres.UpdateGenre;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Api.Controllers.Genres
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ISender _sender;
        public GenresController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetGenreByIdQuery(id);

            Result<GenreResponse> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres(CancellationToken cancellationToken)
        {
            var query = new GetGenresQuery();

            Result<IEnumerable<GenreResponse>> result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(
            AddGenreRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddGenreCommand(request.Name);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetGenres), new { id = result.Value }, result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGenre(
            UpdateGenreRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateGenreCommand(
                request.GenreId,
                request.Name);

            Result result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(
            Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteGenreCommand(id);

            Result result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
