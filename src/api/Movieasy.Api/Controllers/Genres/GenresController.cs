﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Genres.AddGenre;
using Movieasy.Application.Genres.DeleteGenre;
using Movieasy.Application.Genres.GetGenre;
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