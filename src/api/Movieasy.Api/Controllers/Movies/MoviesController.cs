﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Actors.GetActorById;
using Movieasy.Application.Common;
using Movieasy.Application.Movies.AddMovie;
using Movieasy.Application.Movies.DeleteMovie;
using Movieasy.Application.Movies.GetMovie;
using Movieasy.Application.Movies.GetMovieActorsQuery;
using Movieasy.Application.Movies.GetMovieById;
using Movieasy.Application.Movies.UpdateMovie;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Api.Controllers.Movies
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ISender _sender; 
        public MoviesController(ISender sender)
        {
            _sender = sender;
        }


        // TO DO: Make sure to add an object representing the input parameters (may or may not be a good idea)
        [HttpGet]
        public async Task<IActionResult> GetMovies(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            CancellationToken cancellationToken,
            int pageNumber = 1,
            int pageSize = 12)
        {
            var query = new GetMoviesQuery(searchTerm, sortColumn, sortOrder, pageNumber, pageSize);

            Result<PagedList<MovieResponse>> result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet("{id}/actors")]
        public async Task<IActionResult> GetMovieActors(
          Guid id,
          CancellationToken cancellationToken) // movieId
        {
            var query = new GetMovieActorsQuery(id);

            Result<IEnumerable<ActorResponse>> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetMovieByIdQuery(id);

            Result<MovieResponse> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AddMovie(
           [FromForm] AddMovieRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddMovieCommand(
                request.Title,
                request.Description,
                request.TrailerUrl,
                request.Rating,
                request.ReleaseDate,
                request.Duration,
                request.Genres,
                request.Actors,
                request.Photo);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetMovie), new { id = result.Value }, result.Value);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateMovie(
            [FromForm] UpdateMovieRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateMovieCommand(
                request.MovieId,
                request.Title,
                request.Description,
                request.TrailerUrl,
                request.Rating,
                request.ReleaseDate,
                request.Duration,
                request.Genres,
                request.Actors,
                request.Photo);

            Result result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteMovie(
            Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteMovieCommand(id);

            Result result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
